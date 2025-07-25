namespace Identity.Application.Auth.User.Command.Login;

public class LoginUserResponse
{    
    public string Token { get; private set; }
    
    public string RefreshToken { get; private set; }
    
    public DateTime ExpiresIn { get; private set; }

}