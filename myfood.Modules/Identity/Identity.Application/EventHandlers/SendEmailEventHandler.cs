using Identity.Domain.Event;
using Shared.Domain.CQRS;

namespace Identity.Application.EventHandlers;

public class SendEmailEventHandler: IDomainEventHandler<SendEmailDomainEvent>
{
    public Task Handle(SendEmailDomainEvent domainEvent, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}