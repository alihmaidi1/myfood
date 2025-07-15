using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Refit;
using Shared.Domain.Services.Email;
using Shared.Domain.Services.Twilio;
using Shared.Infrastructure.Extensions;
using Shared.Infrastructure.Security;
using Shared.Infrastructure.Security.Jwt;
using Shared.Infrastructure.Services.Email;
using Shared.Infrastructure.Services.Twilio;
using Shared.Infrastructure.Services.Whatsapp;

namespace Shared.Infrastructure;

public static class InfrastructureConfiguration
{


    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {

        services.AddLimitRate();
        services.AddJwtConfiguration(configuration);
        
        services.AddOptions<JwtSetting>()
            .BindConfiguration("Jwt")
            .ValidateDataAnnotations()
            .ValidateOnStart();
        
        services.AddScoped<ISmsTwilioService,SmsTwilioService>();
        
        services.AddOptions<TwilioSmsSetting>()
            .BindConfiguration("Twilio")
            .ValidateDataAnnotations()
            .ValidateOnStart();
        
        services.AddScoped<IMailService, MailService>();

        services.AddOptions<MailSetting>()
            .BindConfiguration("Email")
            .ValidateDataAnnotations()
            .ValidateOnStart();
        
    
        services.AddScoped<IWhatsAppService, WhatsAppService>();
        services.AddRefitClient<IWhatsAppCloudApi>()
            .ConfigureHttpClient(c => c.BaseAddress = new Uri(configuration["WhatsApp:BaseUrl"]!))
            .AddPolicyHandler(PollyExtensions.GetTimeOutPolicy(100))
            .AddPolicyHandler(c=>PollyExtensions.GetRetryPolicy());

    
    
        services.AddOptions<WhatsappMessageSetting>()
            .BindConfiguration("Whatsapp")
            .ValidateDataAnnotations()
            .ValidateOnStart();



        
        return services;
    }
    
    
    public static WebApplication UseInfrastructure(this WebApplication app)
    {
        app.UseRateLimiter();

        app.UseAuthentication();
   
        

        return app;
    }

}