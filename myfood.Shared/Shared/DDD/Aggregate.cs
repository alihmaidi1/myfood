namespace Shared.DDD;

public class Aggregate: Entity,IAggregate
{

    public List<IDomainEvent> _domainEvents { get; } = new();
    
    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }

    
    
    public void RaiseDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }
}