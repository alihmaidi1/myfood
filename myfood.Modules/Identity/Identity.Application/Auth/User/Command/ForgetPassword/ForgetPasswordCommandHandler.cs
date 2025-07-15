using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Shared.Domain.CQRS;
using Shared.Domain.Extensions;
using Shared.Domain.OperationResult;
using Shared.Domain.Services.Twilio;


namespace Identity.Application.Auth.User.Command.ForgetPassword;

internal sealed class ForgetPasswordCommandHandler: ICommandHandler<ForgetPasswordCommand>
{

    private readonly UserManager<Domain.Security.User>  _userManager;
    // private readonly Iemailser  _smsTwilioService;

    public ForgetPasswordCommandHandler(UserManager<Domain.Security.User>  userManager,ISmsTwilioService  smsTwilioService)
    {
        _userManager = userManager;
        // _smsTwilioService = smsTwilioService;
    }
    public async Task<IResult> Handle(ForgetPasswordCommand request, CancellationToken cancellationToken)
    {
        var user=await _userManager.FindByEmailAsync(request.Email);
        if (user == null)
        {
            
            return Result.ValidationFailure<bool>(Error.ValidationFailures("Email or Password is not valid.")).ToActionResult();
        }
        var result=user.SetForgetCode(string.Empty.GenerateRandomString(5),5);
        if (result.IsFailure)
        {
            return result.ToActionResult();
        }
        
        
        
        await _userManager.UpdateAsync(user);
        
        return Result.Success(true).ToActionResult();
    }
}