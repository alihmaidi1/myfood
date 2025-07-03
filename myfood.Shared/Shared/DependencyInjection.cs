using Shared.Extensions;
using Shared.Security;
using Shared.Versioning;

namespace Shared;

public static class DependencyInjection
{

    public static IServiceCollection AddShared(this IServiceCollection services, IConfiguration configuration,
        params Assembly[] assemblies)
    {

        services.AddVersioning();
        services.Scan(scan =>
            scan.FromAssemblies(assemblies)
                .AddClasses(classes => classes.AssignableTo(typeof(IRequestHandler<>)), publicOnly: false)
                .AsImplementedInterfaces()
                .WithScopedLifetime()
                .AddClasses(classes => classes.AssignableTo(typeof(IDomainEventHandler<>)), publicOnly: false)
                .AsImplementedInterfaces()
                .WithScopedLifetime()
        
        );
        services.AddScoped<GlobalExceptionHandlingMiddleware>();
        services.AddValidatorsFromAssemblies(assemblies);
        // services.AddCarterWithAssemblies(assemblies);
        services.AddLimitRate();
        services.AddScoped<IDomainEventDispatcher, DomainEventDispatcher>();
        services.TryDecorate(typeof(ICommandHandler<>), typeof(ValidationDecorator.CommandHandler<>));
        services.TryDecorate(typeof(IQueryHandler<>), typeof(ValidationDecorator.QueryHandler<>));
        services.TryDecorate(typeof(ICommandHandler<>), typeof(LoggingDecorator.CommandHandler<>));
        services.TryDecorate(typeof(IQueryHandler<>), typeof(LoggingDecorator.QueryHandler<>));
        return services;
    
    }


    public static WebApplication UseShared(this WebApplication app)
    {
        app.UseRateLimiter();
        // app.MapCarter();
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/openapi/v1.json", "");
        
        });    
        return app;
    }

}