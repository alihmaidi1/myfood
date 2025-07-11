using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

// using Microsoft.ent
namespace MyFood.IntegrationTest;

public class IntegrationTestWebAppFactory: WebApplicationFactory<Program>
{

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {


        builder.ConfigureTestServices(Services =>
        {
            var descriptor = Services.SingleOrDefault(x => x.ServiceType == typeof(DbContextOptions<>));
            if (descriptor != null)
            {
                
                Services.Remove(descriptor);
            }

            // Services.AddDbContext<>();

        });
    }
}