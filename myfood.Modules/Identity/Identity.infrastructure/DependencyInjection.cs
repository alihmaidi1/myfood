using Identity.Domain.Repository;
using Identity.Domain.Security;
using Identity.infrastructure.Repositories;
using Identity.infrastructure.Repositories.Jwt;
using Identity.infrastructure.Seed;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Infrastructure.BackgroundJobs;
using Shared.Infrastructure.Database;

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
            .AddEntityFrameworkStores<myFoodIdentityDbContext>()
            .AddSignInManager()
            .AddDefaultTokenProviders()
            .AddApiEndpoints();

        services.AddDbContext<myFoodIdentityDbContext>(Postgres.StandardOptions(configuration, Schemas.Identity));
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        
        services.AddHostedService<OutboxProcessor<myFoodIdentityDbContext>>();
        return services;
    }
    
    
    public static WebApplication UseIdentityInfrastructureModule(this WebApplication app)
    {
    
        using(var scope= app.Services.CreateScope()){
        
            IdentityDatabaseSeed.InitializeAsync(scope.ServiceProvider).GetAwaiter().GetResult();
        }

        return app;
    }

}