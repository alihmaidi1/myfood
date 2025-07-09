using Carter;

using Refit;
using Shared.Extensions;
using Shared.Security;
using Shared.Security.Jwt;
using Shared.Services.Email;
using Shared.Services.File;
using Shared.Services.Twilio;
using Shared.Services.User;
using Shared.Services.Whatsapp;
using Shared.Versioning;

namespace Shared;

public static class DependencyInjection
{

    public static IServiceCollection AddShared(this IServiceCollection services, IConfiguration configuration,
        params Assembly[] assemblies)
    {


        #region Services
        
            services.AddHttpContextAccessor();
            services.AddScoped<ICurrentUserService,CurrentUserService>();
            services.AddScoped<IAwsStorageService,AwsStorageService>();
            services.AddScoped<IWhatsAppService, WhatsAppService>();
            services.AddScoped<ISmsTwilioService,SmsTwilioService>();
            services.AddScoped<IMailService, MailService>();

        #endregion
        
        services.AddVersioning();
        services.AddMemoryCache();
        
        
        services.AddRefitClient<IWhatsAppCloudApi>()
            .ConfigureHttpClient(c => c.BaseAddress = new Uri(configuration["WhatsApp:BaseUrl"]!))
            .AddPolicyHandler(PollyExtensions.GetTimeOutPolicy(100))
            .AddPolicyHandler(c=>PollyExtensions.GetRetryPolicy());

        
        
        services.AddOptions<WhatsappMessageSetting>()
            .BindConfiguration("Whatsapp")
            .ValidateDataAnnotations()
            .ValidateOnStart();
        
        services.AddOptions<JwtSetting>()
            .BindConfiguration("Jwt")
            .ValidateDataAnnotations()
            .ValidateOnStart();
        
        
        
        services.AddOptions<TwilioSmsSetting>()
            .BindConfiguration("Twilio")
            .ValidateDataAnnotations()
            .ValidateOnStart();
        
        services.AddOptions<AwsS3Setting>()
            .BindConfiguration("AwsS3")
            .ValidateDataAnnotations()
            .ValidateOnStart();
        
        
        services.AddOptions<MailSetting>()
            .BindConfiguration("Email")
            .ValidateDataAnnotations()
            .ValidateOnStart();
        services.AddScoped<GlobalExceptionHandlingMiddleware>();
        services.AddValidatorsFromAssemblies(assemblies);
        services.AddCarterWithAssemblies(assemblies);
        services.AddLimitRate();
        services.AddJwtConfiguration(configuration);

        #region CQRS_Abstraction
            
            services.Scan(scan =>
                scan.FromAssemblies(assemblies)
                    .AddClasses(classes => classes.AssignableTo(typeof(IRequestHandler<>)), publicOnly: false)
                    .AsImplementedInterfaces()
                    .WithScopedLifetime()
                    .AddClasses(classes => classes.AssignableTo(typeof(IDomainEventHandler<>)), publicOnly: false)
                    .AsImplementedInterfaces()
                    .WithScopedLifetime()
            
            );
            services.AddScoped<IDomainEventDispatcher, DomainEventDispatcher>();
            services.TryDecorate(typeof(ICommandHandler<>), typeof(ValidationDecorator.CommandHandler<>));
            services.TryDecorate(typeof(IQueryHandler<>), typeof(ValidationDecorator.QueryHandler<>));
            services.TryDecorate(typeof(ICommandHandler<>), typeof(LoggingDecorator.CommandHandler<>));
            services.TryDecorate(typeof(IQueryHandler<>), typeof(LoggingDecorator.QueryHandler<>));
            services.TryDecorate(typeof(ICommandHandler<>), typeof(IdempotencyDecorator.CommandHandler<>));

        

        #endregion

        
        return services;
    
    }


    public static WebApplication UseShared(this WebApplication app)
    {
        app.UseRateLimiter();

        app.UseAuthentication();
        
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/openapi/v1.json", "");
        
        });
        app.MapCarter();

        return app;
    }

}