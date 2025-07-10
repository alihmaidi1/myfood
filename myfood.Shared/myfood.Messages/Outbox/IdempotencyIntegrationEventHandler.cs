using MassTransit;
using Microsoft.EntityFrameworkCore;
using myfood.Messages.Events;

namespace myfood.Messages.Outbox;

public class IdempotencyIntegrationEventHandler<T,TContext>: IConsumer<T> where T : IntegrationEvent where TContext : DbContext
{
    
    private readonly IConsumer<T> _consumer;
     private readonly TContext _context;

    public IdempotencyIntegrationEventHandler(IConsumer<T> consumer,TContext context)
    {
        
        _consumer = consumer;
        _context = context;
    }
    
    public async Task Consume(ConsumeContext<T> context)
    {
        if (await _context.Set<OutboxConsumer>().AnyAsync(x=>x.Id==context.MessageId&&x.Name==_consumer.GetType().Name))
        {
            return;            
        }
        await _consumer.Consume(context);

        await _context.Set<OutboxConsumer>().AddAsync(new OutboxConsumer()
        {
            Id = (Guid)context.MessageId!,
            Name = _consumer.GetType().Name,

        });
        await _context.SaveChangesAsync();
    }
}