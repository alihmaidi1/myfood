using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Shared.Authorization;

public class UsertypeAuthorize: AuthorizeAttribute, IAuthorizationFilter
{
    
    private UserType _userType;
    public UsertypeAuthorize(UserType userType)
    {
        _userType=userType;
        
    }
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        
        var userType=context.HttpContext.User.Claims.FirstOrDefault(c => c.Type=="UserType")?.Value;
        if (userType!=_userType.ToString())
        {
            context.Result = Result.Failure<object>(Error.InvalidUserType,HttpStatusCode.Forbidden).ToJsonResult();

        }
    }
}