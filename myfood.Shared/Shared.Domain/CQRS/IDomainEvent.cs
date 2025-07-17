namespace Shared.Domain.CQRS;

public interface IDomainEvent
{
    public Guid EventId => Guid.NewGuid();
    public DateTime OccurredOn=> DateTime.UtcNow;
}