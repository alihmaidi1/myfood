using Identity.Domain.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Shared.Authorization;
using Shared.Contract.CQRS;
using Shared.OperationResult;

namespace Identity.Application.Auth.User.Command.Login;

public class LoginUserHandler: ICommandHandler<LoginUserCommand>
{
    
    private readonly UserManager<Domain.Security.User>  _userManager;
    private readonly IJwtRepository _jwtRepository;
    public LoginUserHandler(UserManager<Domain.Security.User>  userManager,IJwtRepository jwtRepository)
    {
        _userManager = userManager;
        _jwtRepository = jwtRepository;

    }
    
    public async Task<IResult> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var user=await _userManager.FindByEmailAsync(request.Email);
        if (user == null)
        {
            
            return Result.ValidationFailureResult<LoginUserResponse>(Error.ValidationFailures("Email or Password is not valid."));
        }

        if (!user.EmailConfirmed)
        {
            return Result.ValidationFailureResult<LoginUserResponse>(Error.ValidationFailures("your email is not confirmed"));
            
        }
        var passwordValid = await _userManager.CheckPasswordAsync(user, request.Password);
        if (!passwordValid)
        {
            return Result.ValidationFailureResult<LoginUserResponse>(Error.ValidationFailures("Email or Password is not valid."));
            
        }

        var result = await _jwtRepository.GetTokensInfo(user.Id,user.Email!,UserType.Customer,cancellationToken);
        
        return Result.SuccessResult(result);
    }
}