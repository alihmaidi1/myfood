using Identity.Domain.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Shared.Authorization;
using Shared.Contract.CQRS;
using Shared.OperationResult;

namespace Identity.Application.Auth.User.Command.Login;

public class LoginUserHandler: ICommandHandler<LoginUserRequest>
{
    
    private readonly UserManager<Domain.Security.User>  _userManager;
    private readonly IJwtRepository _jwtRepository;
    public LoginUserHandler(UserManager<Domain.Security.User>  userManager,IJwtRepository jwtRepository)
    {
        _userManager = userManager;
        _jwtRepository = jwtRepository;

    }
    
    public async Task<JsonResult> Handle(LoginUserRequest request, CancellationToken cancellationToken)
    {
        var user=await _userManager.FindByEmailAsync(request.Email);
        if (user == null)
        {
            
            return Result.ValidationFailure<LoginUserResponse>(Error.ValidationFailures("Email or Password is not valid.")).ToJsonResult();
        }

        if (!user.EmailConfirmed)
        {
            return Result.ValidationFailure<LoginUserResponse>(Error.ValidationFailures("your email is not confirmed")).ToJsonResult();
            
        }
        var passwordValid = await _userManager.CheckPasswordAsync(user, request.Password);
        if (!passwordValid)
        {
            return Result.ValidationFailure<LoginUserResponse>(Error.ValidationFailures("Email or Password is not valid.")).ToJsonResult();
            
        }

        var result = await _jwtRepository.GetTokensInfo(user.Id,user.Email!,UserType.Customer);
        
        return Result.Success(result).ToJsonResult();
    }
}