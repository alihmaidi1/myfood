using Shared.Domain.MediatR;
using Shared.Domain.OperationResult;

namespace Identity.Application.Auth.User.Command.ForgetPassword;

public class ForgetPasswordRequest
{
    
    public string Email { get; set; }
    
}


public sealed class ForgetPasswordCommand : ForgetPasswordRequest,ICommand<TResult<bool>>
{
    public Guid? RequestId { get; set; }
}