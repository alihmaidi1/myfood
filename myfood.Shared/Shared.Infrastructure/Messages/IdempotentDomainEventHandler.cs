using Microsoft.EntityFrameworkCore;
using Shared.Domain.CQRS;
using Shared.Domain.Entities;

namespace Shared.Infrastructure.Messages;

public class IdempotentDomainEventHandler<TDomainEvent,TDbContext>:IDomainEventHandler<TDomainEvent> where TDomainEvent : IDomainEvent
where TDbContext : DbContext
{
    private readonly IDomainEventHandler<TDomainEvent> _decorated;
    private readonly TDbContext  _dbContext;
    public IdempotentDomainEventHandler(TDbContext  dbContext,IDomainEventHandler<TDomainEvent> decorated)
    {
        _decorated = decorated;
        _dbContext = dbContext;
    }
    
    
    public async Task Handle(TDomainEvent domainEvent, CancellationToken cancellationToken)
    {
        var isProccessed=_dbContext.Set<OutboxMessageConsumer>().Any(x=>x.OutboxMessageId==domainEvent.EventId&&x.Name==domainEvent.GetType().AssemblyQualifiedName);
        if (!isProccessed) return;
        await _decorated.Handle(domainEvent, cancellationToken);
        _dbContext.Set<OutboxMessageConsumer>().Add(new OutboxMessageConsumer(domainEvent.EventId, domainEvent.GetType().AssemblyQualifiedName!));
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}