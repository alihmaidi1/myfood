using Shared.Contract.CQRS;

namespace Identity.Application.Auth.User.Command.Login;

public class LoginUserRequest: ICommand
{
    public string Email { get; set; }
    public string Password { get; set; }
    
}