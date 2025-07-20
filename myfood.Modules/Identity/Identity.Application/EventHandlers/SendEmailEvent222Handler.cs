using Identity.Domain.Event;
using Shared.Domain.Event;

namespace Identity.Application.EventHandlers;

public class SendEmailEvent222Handler:IEventHandler<SendEmai22lEvent>
{
    public async Task Handle(SendEmai22lEvent domainEvent, CancellationToken cancellationToken)
    {
        Console.WriteLine("dfdffd");
    }
}