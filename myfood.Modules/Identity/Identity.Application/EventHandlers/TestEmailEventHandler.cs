using Identity.Domain.Event;
using Shared.Domain.Event;

namespace Identity.Application.EventHandlers;

internal sealed class TestEmailEventHandler:IEventHandler<SendEmailEvent>
{
    public async Task Handle(SendEmailEvent domainEvent, CancellationToken cancellationToken)
    {
        
        Console.WriteLine("TestEmailEventHandler1");
    }
}