// using Identity.Domain.Repository;
// using Identity.Domain.Security;
// using Mapster;
// using Microsoft.AspNetCore.Http;
// using Microsoft.AspNetCore.Identity;
// using Shared.Domain.CQRS;
// using Shared.Domain.OperationResult;
// using Shared.Domain.Services;
//
// namespace Identity.Application.Auth.User.Command.Login;
//
// internal sealed class LoginUserCommandHandler: ICommandHandler<LoginUserCommand,TResult<LoginUserResponse>>
// {
//     
//     private readonly IUnitOfWork  _unitOfWork;
//     public LoginUserCommandHandler(IUnitOfWork  unitOfWork)
//     {
//         _unitOfWork=unitOfWork;
//
//     }
//     
//     public async Task<TResult<LoginUserResponse>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
//     {
//         var user=await _unitOfWork._userManager.FindByEmailAsync(request.Email).WaitAsync(cancellationToken);
//         var passwordValid = await _unitOfWork._userManager.CheckPasswordAsync(user, request.Password).WaitAsync(cancellationToken);
//         if (user is null||!passwordValid)
//         {
//             return Result.ValidationFailure<LoginUserResponse>(Error.ValidationFailures("Email or Password is not valid."));
//             
//         }
//         
//         if (!user.EmailConfirmed)
//         {
//             return Result.ValidationFailure<LoginUserResponse>(Error.ValidationFailures("your email is not confirmed"));
//             
//         }
//
//         var result = await _unitOfWork._jwtRepository.GetTokensInfo(user.Id,user.Email!,UserType.Customer,cancellationToken);
//         await _unitOfWork.SaveChangesAsync(cancellationToken);
//         return Result.Success(result.Adapt<LoginUserResponse>());
//     }
// }