using FluentValidation;
using Identity.Application.Auth.User.Command.Login;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Domain.CQRS;

namespace Identity.Application;

public static class DependencyInjection
{

    public static IServiceCollection AddIdentityApplicationModules(this IServiceCollection services, IConfiguration configuration)
    {
        
        return services;
    }

    public static WebApplication UseIdentityApplicationModule(this WebApplication app)
    {

        // app.UseIdentityInfrastructureModule();
        return app;
    }

}