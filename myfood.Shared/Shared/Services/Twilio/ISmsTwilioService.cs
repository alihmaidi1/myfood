using Twilio.Rest.Api.V2010.Account;

namespace Shared.Services.Twilio;

public interface ISmsTwilioService
{

    public Task<MessageResource> Send(string mobileNumber,string Body);
    
    
    
}
