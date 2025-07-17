using Shared.Domain.CQRS;

namespace Shared.Domain.Entities;

public interface IAggregate: IEntity
{
    public List<IDomainEvent> GetDomainEvents();
    
    IDomainEvent[] ClearDomainEvents();
    public void RaiseDomainEvent(IDomainEvent domainEvent);

}