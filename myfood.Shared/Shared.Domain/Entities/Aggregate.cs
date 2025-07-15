using Shared.Domain.CQRS;

namespace Shared.Domain.Entities;

public class Aggregate: Entity,IAggregate
{

    public List<IDomainEvent> _domainEvents { get; } = new List<IDomainEvent>();
    public IDomainEvent[] ClearDomainEvents()
    {
        
        IDomainEvent[] dequeuedEvents = _domainEvents.ToArray();
        _domainEvents.Clear();
        return dequeuedEvents;
    }

    public void RaiseDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
        
        
    }
}