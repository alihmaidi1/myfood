using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.Contract.CQRS;
using Shared.Services.User;

namespace Identity.Application.Auth.User.Command.ChangePassword;

public class ChangePasswordHandler: ICommandHandler<ChangePasswordRequest>
{
    private readonly ICurrentUserService _currentUserService;
    public ChangePasswordHandler(ICurrentUserService  currentUserService)
    {
        
        _currentUserService=currentUserService;
    }
    
    public async Task<IResult> Handle(ChangePasswordRequest request, CancellationToken cancellationToken)
    {

        Console.WriteLine(_currentUserService.UserId);
        return Results.Ok("sdfs");
        // return ("password changed");
    }
}