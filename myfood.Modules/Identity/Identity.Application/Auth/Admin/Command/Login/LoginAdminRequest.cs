using Identity.Application.Auth.User.Command.Login;
using Shared.Domain.CQRS;
using Shared.Domain.OperationResult;

namespace Identity.Application.Auth.Admin.Command.Login;



public class LoginAdminRequest
{
    public string Email { get; set; }
    public string Password { get; set; }
    
}

public sealed class LoginAdminCommand:LoginAdminRequest, ICommand<TResult<LoginAdminResponse>>
{
    public Guid? RequestId { get; set; }
}