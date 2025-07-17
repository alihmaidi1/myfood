using Shared.Domain.CQRS;

namespace Identity.Domain.Event;

public sealed class SendEmailDomainEvent: IDomainEvent
{
    public string Email;
    public string Message;

    public SendEmailDomainEvent(string email, string message)
    {
        Email = email;
        Message = message;
        
    }

}