using System.Reflection;
using Carter;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Shared.Application.CQRS;
// using Shared.Application.Decorator;
using Shared.Application.Services.User;
using Shared.Application.Versioning;
using Shared.Domain.CQRS;
using Shared.Extensions;

namespace Shared.Application;

public static class ApplicationConfiguration
{

    public static IServiceCollection AddApplication(
        this IServiceCollection services,
        Assembly[] moduleAssemblies)
    {

        #region Core
        services.AddVersioning();
        services.AddMemoryCache();
        services.AddCarterWithAssemblies(moduleAssemblies);

        #endregion        
        
        #region CQRS_Abstraction
        services.Scan(scan =>
            scan.FromAssemblies(moduleAssemblies)
                .AddClasses(classes => classes.AssignableTo(typeof(IRequestHandler<,>)), publicOnly: false)
                .AsImplementedInterfaces()
                .WithScopedLifetime()
                .AddClasses(classes => classes.AssignableTo(typeof(IPipelineBehavior<,>)), publicOnly: false)
                .AsImplementedInterfaces()
                .WithScopedLifetime()
                .AddClasses(classes => classes.AssignableTo(typeof(IDomainEventHandler<>)), publicOnly: false)
                .AsImplementedInterfaces()
                .WithScopedLifetime()
                
        );
        services.AddScoped<IDispatcher, Dispatcher>();
        services.AddSingleton<IDomainEventDispatcher, DomainEventDispatcher>();
        services.AddValidatorsFromAssemblies(moduleAssemblies,includeInternalTypes:true);
        
        
        #endregion

        #region Services

        services.AddHttpContextAccessor();
        services.AddScoped<ICurrentUserService,CurrentUserService>();


        #endregion

        return services;
    }


    public static WebApplication UseApplication(this WebApplication app)
    {
        
        
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/openapi/v1.json", "");
        
        });
        app.MapCarter();
        
        return app;
    }
}