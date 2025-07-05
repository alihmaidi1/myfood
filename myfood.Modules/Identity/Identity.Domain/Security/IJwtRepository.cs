using Shared.Authorization;

namespace Identity.Domain.Security;

public interface IJwtRepository
{
    
    public Task<TokenInfo> GetTokensInfo(Guid id,string email,UserType type,CancellationToken cancellationToken,List<string>? permissions=null);

    public string GetToken(Guid id,string email,UserType type,List<string>? permissions);
}