using Microsoft.AspNetCore.Mvc;
using Shared.Contract.CQRS;
using Shared.OperationResult;

namespace Identity.Application.Auth.User.Command.Login;

public class LoginUserHandler: ICommandHandler<LoginUserRequest>
{
    
    
    public async Task<JsonResult> Handle(LoginUserRequest request, CancellationToken cancellationToken)
    {
        return Result.Success<object>(null).ToJsonResult();
    }
}