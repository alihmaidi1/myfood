using System.Security.Claims;

namespace Shared.Services.User;

public interface ICurrentUserService
{
    
    string? UserId { get; }
    string? UserName { get; }
    bool IsAuthenticated { get; }
    IEnumerable<Claim> Claims { get; }
}