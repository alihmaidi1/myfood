using Shared.Domain.CQRS;
using Shared.Infrastructure.Messages;

namespace Identity.infrastructure.Messages;

public class IdempotentIdentityDomainEventHandler<TDomainEvent>: IdempotentDomainEventHandler<TDomainEvent,myFoodIdentityDbContext> where TDomainEvent : IDomainEvent
{
    public IdempotentIdentityDomainEventHandler(myFoodIdentityDbContext dbContext, IDomainEventHandler<TDomainEvent> decorated) : base(dbContext, decorated)
    {
    }
}