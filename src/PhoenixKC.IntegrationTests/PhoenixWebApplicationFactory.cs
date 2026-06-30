using Respawn;
using Testcontainers.MsSql;
using PhoenixKC.Infrastructure;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

namespace PhoenixKC.IntegrationTests;

public sealed class PhoenixWebApplicationFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    #region Instance
    private MsSqlContainer Container { get; }
    private string ConnectionString
    {
        get => field ??= Container.GetConnectionString();
    }
    private SqlConnection SqlConnection
    {
        get
        {
            if(field is null)
            {
                field = new SqlConnection(ConnectionString);
                field.Open();
            }
            return field;
        }
    }
    private Respawner Respawner
    {
        get => field ??= Respawner.CreateAsync(SqlConnection, new RespawnerOptions()
        {
            DbAdapter = DbAdapter.SqlServer,
            TablesToIgnore = ["__EFMigrationsHistory"]
        }).GetAwaiter().GetResult();
    }

    public PhoenixWebApplicationFactory()
    {
        Container = new MsSqlBuilder("mcr.microsoft.com/mssql/server:2022-latest").WithPassword("Password123!").Build();
    }

    public async ValueTask ResetDatabase()
    {
        await Respawner.ResetAsync(SqlConnection);
    }
    #endregion

    #region Base
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            // Remove the default PhoenixDbContext registration
            Type descriptor_type = typeof(DbContextOptions<PhoenixDbContext>);
            ServiceDescriptor? descriptor = services.SingleOrDefault(s => s.ServiceType == descriptor_type);
            if(descriptor is not null)
            {
                services.Remove(descriptor);
            }

            // Register PhoenixDbContext with test container connection string
            services.AddDbContext<PhoenixDbContext>(options =>
            {
                options.UseSqlServer(ConnectionString, builder =>
                {
                    builder.MigrationsAssembly(typeof(PhoenixDbContext).Assembly.GetName().Name);
                });
            });
        });
    }
    #endregion

    #region IAsyncLifetime
    public override async ValueTask DisposeAsync()
    {
        await Container.StopAsync();
        await Container.DisposeAsync();
    }
    public async ValueTask InitializeAsync()
    {
        await Container.StartAsync();
        DbContextOptions<PhoenixDbContext> options = new DbContextOptionsBuilder<PhoenixDbContext>().UseSqlServer(
            ConnectionString,
            builder => builder.MigrationsAssembly(typeof(PhoenixDbContext).Assembly.GetName().Name)
        ).Options;

        using var context = new PhoenixDbContext(options);
        await context.Database.MigrateAsync();
    }
    #endregion
}