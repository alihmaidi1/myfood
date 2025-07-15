using Shared.Domain.CQRS;

namespace Identity.Application.Auth.User.Command.ForgetPassword;

public class ForgetPasswordRequest
{
    
    public string Email { get; set; }
    
}


public class ForgetPasswordCommand : ForgetPasswordRequest,ICommand
{
    public Guid? RequestId { get; set; }
}