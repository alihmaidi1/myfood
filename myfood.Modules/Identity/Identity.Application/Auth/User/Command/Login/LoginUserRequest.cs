using Shared.Domain.CQRS;

namespace Identity.Application.Auth.User.Command.Login;

public class LoginUserRequest
{
    public string Email { get; set; }
    public string Password { get; set; }
    
}

public class LoginUserCommand:LoginUserRequest, ICommand
{
    public Guid? RequestId { get; set; }
}