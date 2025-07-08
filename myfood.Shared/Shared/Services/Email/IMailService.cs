namespace Shared.Services.Email;

public interface IMailService 
{
    
    public bool SendMail(string email, string subject, string message);

    
}