using System.Text.Json;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace myfood.Messages.Outbox;

public class OutboxProcessor<TContext>
    (IServiceProvider serviceProvider, IBus bus, ILogger<OutboxProcessor<TContext>> logger)
    : BackgroundService where TContext : DbContext
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        
        while (!stoppingToken.IsCancellationRequested)
        {
                using var scope = serviceProvider.CreateScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<TContext>();
                var outboxMessages = await dbContext.Set<OutboxMessage>()
                    .Where(m => m.ProcessedOn == null)
                    .ToListAsync(stoppingToken);

                foreach (var message in outboxMessages)
                {
                    try
                    {
                        var eventType = Type.GetType(message.Type);
                        if (eventType == null)
                        {
                            logger.LogWarning("Could not resolve type: {Type}", message.Type);
                            continue;
                        }
                        var eventMessage = JsonSerializer.Deserialize(message.Content, eventType);
                        if (eventMessage == null)
                        {
                            logger.LogWarning("Could not deserialize message: {Content}", message.Content);
                            continue;
                        }

                        await bus.Publish(eventMessage, stoppingToken);

                        message.ProcessedOn = DateTime.UtcNow;

                        logger.LogInformation("Successfully processed outbox message with ID: {Id}", message.Id);

                    }
                    catch (Exception e)
                    {
                        message.ErrorMessage = e.Message;
                        
                    }
                    
                }

                await dbContext.SaveChangesAsync(stoppingToken);
                
            await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken); 
        }
    }
}