using Microsoft.AspNetCore.Mvc;
using Shared.Contract.CQRS;

namespace Identity.Application.Auth.User.Command.ChangePassword;

public class ChangePasswordRequest
{
    
    
    public string Password { get; set; }

}

public class ChangePasswordCommand : ChangePasswordRequest,ICommand
{
    public Guid? RequestId { get; set; }
}

