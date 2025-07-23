namespace Identity.Application.Auth.Admin.Command.Login;

public class LoginAdminResponse
{
    public string Token { get; private set; }
    
    public string RefreshToken { get; private set; }
    
    public DateTime ExpiresIn { get; private set; }

    
    public List<string> Permissions { get;  set; }
}