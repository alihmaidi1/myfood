using Identity.Domain.Security;
using Identity.infrastructure.Repositories;
using Identity.infrastructure.Repositories.Jwt;
using Identity.infrastructure.Seed;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using myfood.Messages.Outbox;
using Shared.Services.Archive;

namespace Identity.infrastructure;

public static class DependencyInjection
{

    public static IServiceCollection AddIdentityInfrastructureModule(this IServiceCollection services, IConfiguration configuration)
    {


        services.AddScoped<IJwtRepository, JwtRepository>();
        services.AddScoped<IAccountRepository, AccountRepository>();
        services.AddIdentityCore<User>(option =>
            {
                option.SignIn.RequireConfirmedAccount = true;
                
            
            
            })
            .AddRoles<Role>()
            .AddEntityFrameworkStores<myFoodDbContext>()
            .AddSignInManager()
            .AddDefaultTokenProviders()
            .AddApiEndpoints();

        services.AddDbContext<myFoodDbContext>(option =>
        {
            option.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            
            option.EnableSensitiveDataLogging();

        });

        services.AddHostedService<OutboxProcessor<myFoodDbContext>>();
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