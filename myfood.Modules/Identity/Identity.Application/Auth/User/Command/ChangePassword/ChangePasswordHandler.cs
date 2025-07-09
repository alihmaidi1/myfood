using Microsoft.AspNetCore.Http;
using Shared.Contract.CQRS;
using Shared.Services.User;

namespace Identity.Application.Auth.User.Command.ChangePassword;

public class ChangePasswordHandler: ICommandHandler<ChangePasswordCommand>
{
    private readonly ICurrentUserService _currentUserService;
    public ChangePasswordHandler(ICurrentUserService  currentUserService)
    {
        
        _currentUserService=currentUserService;
    }
    
    public async Task<IResult> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
    {

        Console.WriteLine(_currentUserService.UserId);
        return Results.Ok("sdfs");
        // return ("password changed");
    }
}