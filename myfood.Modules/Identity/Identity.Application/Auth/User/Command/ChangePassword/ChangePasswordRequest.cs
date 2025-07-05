using Shared.Contract.CQRS;

namespace Identity.Application.Auth.User.Command.ChangePassword;

public class ChangePasswordRequest: ICommand
{
    
    public string Password { get; set; }
    
}