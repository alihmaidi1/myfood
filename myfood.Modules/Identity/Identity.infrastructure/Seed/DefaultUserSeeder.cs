using Identity.Domain.Enum;
using Identity.Domain.Security;
using Identity.Domain.Security.Admin;
using Microsoft.AspNetCore.Identity;
using Shared.Domain.Services.Hash;

namespace Identity.infrastructure.Seed;

public class DefaultUserSeeder
{
    
    public static async Task seedData(myFoodIdentityDbContext context,IWordHasherService  hasherService)
    {

        if (!context.Admins.Any())
        {
            var role = Role.Create(nameof(StaticRole.SuperAdmin),Enum.GetNames(typeof(Permission)).ToList());
            
            var superAdmin = Admin.Create(nameof(StaticRole.SuperAdmin)+"@gmail.com", "12345678",role,hasherService);
            
            context.Admins.Add(superAdmin);
            await context.SaveChangesAsync();
        }
        
        
    }

}