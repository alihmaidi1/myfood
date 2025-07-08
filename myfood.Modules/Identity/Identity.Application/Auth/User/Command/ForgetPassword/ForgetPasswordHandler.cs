using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Shared.Contract.CQRS;
using Shared.OperationResult;
using Shared.Services.Twilio;

namespace Identity.Application.Auth.User.Command.ForgetPassword;

public class ForgetPasswordHandler: ICommandHandler<ForgetPasswordRequest>
{

    private readonly UserManager<Domain.Security.User>  _userManager;
    private readonly ISmsTwilioService  _smsTwilioService;

    public ForgetPasswordHandler(UserManager<Domain.Security.User>  userManager,ISmsTwilioService  smsTwilioService)
    {
        _userManager = userManager;
        _smsTwilioService = smsTwilioService;
    }
    public async Task<IResult> Handle(ForgetPasswordRequest request, CancellationToken cancellationToken)
    {
        var user=await _userManager.FindByEmailAsync(request.Email);
        if (user == null)
        {
            
            return Result.ValidationFailureResult<bool>(Error.ValidationFailures("Email or Password is not valid."));
        }

        user.ForgetCode = "123456";
        
        await _userManager.UpdateAsync(user);
        
        return Result.SuccessResult(true);
    }
}