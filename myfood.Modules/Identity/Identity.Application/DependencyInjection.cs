using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
namespace Identity.Application;

public static class DependencyInjection
{

    public static IServiceCollection AddIdentityApplicationModules(this IServiceCollection services, IConfiguration configuration)
    {
        // services.AddIdentityInfrastructure(configuration);
        
        
        return services;
    }

    public static WebApplication UseIdentityApplicationModule(this WebApplication app)
    {

        // app.UseIdentityInfrastructureModule();
        return app;
    }

}