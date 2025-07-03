using Identity.Domain.Security;
using Identity.infrastructure.Seed;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Services.Archive;

namespace Identity.infrastructure;

public static class DependencyInjection
{

    public static IServiceCollection AddIdentityInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddIdentity<User,Role>(option =>
            {
                option.SignIn.RequireConfirmedAccount = true;
            
            
            }).AddEntityFrameworkStores<myFoodDbContext>()
            .AddApiEndpoints();

        services.AddDbContext<myFoodDbContext>(option =>
        {
            option.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            
            option.EnableSensitiveDataLogging();

        });
        
        return services;
    }
    
    
    public static WebApplication UseIdentityInfrastructureModule(this WebApplication app)
    {
    
        using(var scope= app.Services.CreateScope()){
        
            DatabaseSeed.InitializeAsync(scope.ServiceProvider).GetAwaiter().GetResult();
        }

        return app;
    }

}