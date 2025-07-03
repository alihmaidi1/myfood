
using Identity.infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
namespace Identity.Application;

public static class DependencyInjection
{

    public static IServiceCollection AddIdentityModules(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddIdentityInfrastructure(configuration);
        
        
        return services;
    }

    public static WebApplication UseIdentityModule(this WebApplication app)
    {

        app.UseIdentityInfrastructureModule();
        return app;
    }

}