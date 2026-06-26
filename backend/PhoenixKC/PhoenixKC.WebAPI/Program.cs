using Serilog;
using FluentValidation;
using PhoenixKC.Infrastructure;
using PhoenixKC.WebAPI.Behaviors;
using PhoenixKC.WebAPI.Extensions;
using Microsoft.EntityFrameworkCore;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog(static void(HostBuilderContext ctx, IServiceProvider provider, LoggerConfiguration cfg) =>
{
    cfg.WriteTo.Console();
});
builder.Services.AddMvcCore();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<PhoenixDbContext>(options =>
{
    string? connection_str = builder.Configuration.GetConnectionString("DefaultConnection");
    ArgumentNullException.ThrowIfNull(connection_str);
    options.UseSqlServer(connection_str, builder =>
    {
        builder.MigrationsAssembly(typeof(PhoenixDbContext).Assembly.GetName().Name);
    });
});
builder.Services.AddMediator(options =>
{
    options.ServiceLifetime = ServiceLifetime.Scoped;
    options.PipelineBehaviors = [typeof(ValidationBehavior<,>)];
});
builder.Services.AddValidatorsFromAssemblyContaining<Program>();

WebApplication app = builder.Build();
if(app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapSwagger();
    app.MapSwaggerUI();
}
using(IServiceScope scope = app.Services.CreateScope())
{
    Console.WriteLine("Applying migrations...");
    await scope.ServiceProvider.GetRequiredService<PhoenixDbContext>().Database.MigrateAsync();
    Console.WriteLine("Migrations applied");
}
app.UseSerilogRequestLogging();
app.MapEndpointsFromAssembly();
app.UseHttpsRedirection();
await app.RunAsync();