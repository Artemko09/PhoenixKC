using PhoenixKC.Infrastructure;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddDbContext<PhoenixDbContext>(options =>
{
    if(builder.Configuration.GetConnectionString("DefaultConnection") is not string connection_str)
    {
        throw new NullReferenceException("Connection string not found");
    }
    options.UseSqlServer(connection_str, builder =>
    {
        builder.MigrationsAssembly(typeof(PhoenixDbContext).Assembly.GetName().Name);
    });
});

var app = builder.Build();
if(app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}
using(IServiceScope scope = app.Services.CreateScope())
{
    Console.WriteLine("Applying migrations...");
    await scope.ServiceProvider.GetRequiredService<PhoenixDbContext>().Database.MigrateAsync();
    Console.WriteLine("Migrations applied");
}
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();