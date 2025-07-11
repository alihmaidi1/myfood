using Identity.infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Testcontainers.MsSql;

namespace Identity.Test.IntegrationTest;

public class IntegrationTestWebAppFactory: WebApplicationFactory<Program>,IAsyncLifetime
{
    
    // private readonly var _dbcontainer=
    private readonly MsSqlContainer _dbContainer = new MsSqlBuilder()
        .WithImage("mcr.microsoft.com/mssql/server:latest")
        .WithPassword("Strong_password_123!")
        .WithName("MsSqlContainerIntegrationTestWebApp")
        .Build();
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        

        builder.ConfigureTestServices(Services =>
        {
            
            var descriptor= Services.SingleOrDefault(x=>x.ServiceType == typeof(DbContextOptions<myFoodIdentityDbContext>));
            if (descriptor is not null)
            {
                Services.Remove(descriptor);
            }
            Services.AddDbContext<myFoodIdentityDbContext>(options =>
            {

                options.UseSqlServer(_dbContainer.GetConnectionString());
            });
            
            
            
            

        });

    }

    public Task InitializeAsync()
    {
        return _dbContainer.StartAsync();

    }

    public new Task DisposeAsync()
    {
        return _dbContainer.StopAsync();

    }
}