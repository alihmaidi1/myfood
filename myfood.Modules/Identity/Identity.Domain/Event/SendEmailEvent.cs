using Shared.Domain.Event;

namespace Identity.Domain.Event;

public sealed class SendEmailEvent: Shared.Domain.Event.Event,IDomainEvent
{
    public string Email;
    public string Message;

    public SendEmailEvent(string email, string message)
    {
        Email = email;
        
        EventId=Guid.NewGuid();
        Message = message;
        
    }

}