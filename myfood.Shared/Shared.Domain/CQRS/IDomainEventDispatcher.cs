namespace Shared.Domain.CQRS;

public interface IDomainEventDispatcher
{
    public Task DispatchAsync(IDomainEvent domainEvents, CancellationToken cancellationToken = default);
    
}