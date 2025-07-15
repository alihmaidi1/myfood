namespace Shared.Domain.CQRS;

public interface IDomainEventDispatcher
{
    public Task DispatchAsync(IEnumerable<IDomainEvent> domainEvents, CancellationToken cancellationToken = default);
    
}