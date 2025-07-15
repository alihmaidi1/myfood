using Identity.Domain.Repository;
using Identity.Domain.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Shared.Domain.CQRS;
using Shared.Domain.OperationResult;

namespace Identity.Application.Auth.User.Command.Login;

internal sealed class LoginUserCommandHandler: ICommandHandler<LoginUserCommand>
{
    
    private readonly UserManager<Domain.Security.User>  _userManager;
    private readonly IJwtRepository _jwtRepository;
    public LoginUserCommandHandler(UserManager<Domain.Security.User>  userManager,IJwtRepository jwtRepository)
    {
        _userManager = userManager;
        _jwtRepository = jwtRepository;

    }
    
    public async Task<IResult> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var user=await _userManager.FindByEmailAsync(request.Email).WaitAsync(cancellationToken);
        var passwordValid = await _userManager.CheckPasswordAsync(user, request.Password).WaitAsync(cancellationToken);
        if (user is null||!passwordValid)
        {
            return Result.ValidationFailure<LoginUserResponse>(Error.ValidationFailures("Email or Password is not valid.")).ToActionResult();
            
        }

        if (!user.EmailConfirmed)
        {
            return Result.ValidationFailure<LoginUserResponse>(Error.ValidationFailures("your email is not confirmed")).ToActionResult();
            
        }

        var result = await _jwtRepository.GetTokensInfo(user.Id,user.Email!,UserType.Customer,cancellationToken);
        
        return Result.Success(result).ToActionResult();
    }
}