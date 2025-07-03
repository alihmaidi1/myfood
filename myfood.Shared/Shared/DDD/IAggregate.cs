namespace Shared.DDD;

public interface IAggregate: IEntity
{
    public List<IDomainEvent> _domainEvents { get; }

    
    void ClearDomainEvents();
    public void RaiseDomainEvent(IDomainEvent domainEvent);

}