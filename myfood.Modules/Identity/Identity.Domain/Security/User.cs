using Identity.Domain.Enum;
using Microsoft.AspNetCore.Identity;
using Shared.Authorization;
using Shared.DDD;

namespace Identity.Domain.Security;

public class User: IdentityUser<Guid>, IEntity
{

    public User()
    {
        
        Id=Guid.NewGuid();
    }
    public UserType UserType { get; set; }=UserType.Customer;


    public string? ForgetCode { get; set; }

    public string CreatedBy { get; set; } = "";
    public DateTime CreatedAt { get; set; }=DateTime.Now;
    public string LastModifiedBy { get; set; } = "";
    public DateTime LastModified { get; set; }=DateTime.Now;
    
    
}