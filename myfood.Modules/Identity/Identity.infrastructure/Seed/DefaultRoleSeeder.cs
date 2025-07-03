using Identity.Domain.Enum;
using Identity.Domain.Security;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shared.Authorization;

namespace Identity.infrastructure.Seed;

public static class DefaultRoleSeeder
{
    
    public static async Task seedData(myFoodDbContext dbContext)
    {
        if (!await dbContext.Roles.AnyAsync())
        {
            dbContext.Roles.AddRange(Enum.GetNames(typeof(StaticRole)).Select(x=>new Role()
            {
                
                Name = x,
                NormalizedName = x.ToUpper()
                
            }).ToList());
            await dbContext.SaveChangesAsync();
            var superadminRole = await dbContext.Roles.FirstAsync(x=>x.Name==nameof(StaticRole.SuperAdmin))!;
            var permissions = Enum.GetNames(typeof(Permission)).Select(x=>new IdentityRoleClaim<Guid>
            {
                ClaimType    = "Permission",
                ClaimValue = x,
                RoleId      = superadminRole.Id,
                
            }).ToList();
            dbContext.RoleClaims.AddRange(permissions);
            await dbContext.SaveChangesAsync();
        }



    }

    
}