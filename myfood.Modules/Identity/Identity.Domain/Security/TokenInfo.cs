namespace Identity.Domain.Security;

public class TokenInfo
{
    
    public string Token { get; set; }
    
    public string RefreshToken { get; set; }
    
    public DateTime ExpiresIn { get; set; }
    
}