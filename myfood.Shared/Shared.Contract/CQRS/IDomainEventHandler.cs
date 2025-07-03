namespace Shared.Contract.CQRS;

public interface IDomainEventHandler<T> where T : IDomainEvent
{
    public Task Handle(T domainEvent, CancellationToken cancellationToken);

    
}