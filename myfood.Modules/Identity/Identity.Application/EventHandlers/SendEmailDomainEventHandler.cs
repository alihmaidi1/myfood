using Identity.Domain.Event;
using Shared.Domain.CQRS;

namespace Identity.Application.EventHandlers;

internal sealed class SendEmailDomainEventHandler: IDomainEventHandler<SendEmailDomainEvent>
{
    public async Task Handle(SendEmailDomainEvent domainEvent, CancellationToken cancellationToken)
    {
        
        Console.WriteLine("SendEmailDomainEventHandler");
    }
}