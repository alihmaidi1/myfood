using Shared.Contract.CQRS;

namespace Identity.Application.Auth.User.Command.ForgetPassword;

public class ForgetPasswordRequest: ICommand
{
    
    public string Email { get; set; }
    
}