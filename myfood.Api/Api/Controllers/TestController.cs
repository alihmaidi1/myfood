using Identity.Application.Auth.User.Command.Login;
using Microsoft.AspNetCore.Mvc;
using Shared.Contract.CQRS;

namespace Api.Controllers;

[Route("Api/SuperAdmin/[controller]/[action]")]
[ApiController]
public class TestController: ControllerBase
{
    
    
    
    /// <summary>
    /// Login admin to dashboard
    /// </summary>
    [HttpPost()]

    public async Task<JsonResult> Login([FromBody] LoginUserRequest command,ICommandHandler<LoginUserRequest> handler,CancellationToken Token)
    {
        var response = await handler.Handle(command,Token);
        return response;

    }


    
    
}