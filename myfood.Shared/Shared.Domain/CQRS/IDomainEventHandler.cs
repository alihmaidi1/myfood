namespace Shared.Domain.CQRS;

public interface IDomainEventHandler<T> where T : IDomainEvent
{
    public Task Handle(T domainEvent, CancellationToken cancellationToken);

    
}