using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;

namespace PhoenixKC.IntegrationTests;

public sealed class PhoenixWebApplicationFactory : WebApplicationFactory<Program>
{
    #region Base
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            
        });
    }
    #endregion
}