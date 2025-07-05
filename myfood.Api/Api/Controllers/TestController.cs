using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
public class TestController: ControllerBase
{

    [Authorize]
    [HttpPost("/ddd")]
    public IActionResult Post()
    {

        return new JsonResult("test");
    }
    
}