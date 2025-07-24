using Shared.Domain.MediatR;
using Shared.Domain.OperationResult;

namespace Identity.Application.Auth.User.Command.Login;

public class LoginUserRequest
{
    public string Email { get; set; }
    public string Password { get; set; }
    
}

public sealed class LoginUserCommand:LoginUserRequest, ICommand<TResult<LoginUserResponse>>
{
    public Guid? RequestId { get; set; }
}