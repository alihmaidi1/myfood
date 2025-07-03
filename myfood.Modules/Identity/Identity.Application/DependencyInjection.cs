using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Identity.Application;

public static class DependencyInjection
{

    public static IServiceCollection AddIdentityModules(this IServiceCollection services, IConfiguration configuration)
    {

        return services;
    }

}