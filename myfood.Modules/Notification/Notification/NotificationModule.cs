using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.SignalR;

namespace Notification;




public static class NotificationModule
{
    
    public static IServiceCollection AddNotificationModule(this IServiceCollection services,
        IConfiguration configuration)
    {

        services.AddSignalR(options =>
        {
            
            options.EnableDetailedErrors = true;
            
        });
        services.AddSingleton<IUserIdProvider, UserIdProvider>();
        
        return services;

    }

    public static WebApplication UseNotificationModule(this WebApplication app)
    {

        return app;
    }
    
}