using Identity.Domain.Repository;
using Identity.Domain.Security;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Shared.Domain.CQRS;
using Shared.Domain.OperationResult;
using Shared.Domain.Services.Hash;

namespace Identity.Application.Auth.Admin.Command.Login;

internal sealed class LoginAdminCommandHandler: ICommandHandler<LoginAdminCommand,TResult<LoginAdminResponse>>
{
    
    private readonly IUnitOfWork  _unitOfWork;
    private IWordHasherService _wordHasherService;
    public LoginAdminCommandHandler(IUnitOfWork unitOfWork,IWordHasherService  wordHasherService)
    {
        _wordHasherService= wordHasherService;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<TResult<LoginAdminResponse>> Handle(LoginAdminCommand request, CancellationToken cancellationToken)
    {

        var user = await _unitOfWork
            ._adminRepository.GetQueryable()
            .Include(x=>x.Roles)
            .FirstOrDefaultAsync(x=>x.Email==request.Email,cancellationToken);
            
            
        if (user is null||!_wordHasherService.IsValid(request.Password,user.Password))
        {
            return Result.ValidationFailure<LoginAdminResponse>(Error.ValidationFailures("Email or Password is not valid."));
            
        }

        var permissions = user.Roles.SelectMany(x=>x.Permissions).ToList();
        var tokenInfo = await _unitOfWork._jwtRepository.GetTokensInfo(user.Id,user.Email!,UserType.Customer,cancellationToken,permissions);
        var result = tokenInfo.Adapt<LoginAdminResponse>();
        result.Permissions = permissions;
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success(result);

    }
}