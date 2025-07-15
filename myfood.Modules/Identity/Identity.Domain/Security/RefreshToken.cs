using Shared.Domain.Entities;

namespace Identity.Domain.Security;

public class RefreshToken: Entity,IEntity
{

    public RefreshToken()
    {
        
        Id=Guid.NewGuid();
    }
    
    public string Value { get; set; }
    
    
    
}