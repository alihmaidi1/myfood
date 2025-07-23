using Identity.Domain.Security;
using Identity.Domain.Security.Admin;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Shared.Domain.Services.Hash;

namespace Identity.infrastructure.Seed;

public static class IdentityDatabaseSeed
{


    public static async Task InitializeAsync(IServiceProvider services)
    {
        var context = services.GetRequiredService<myFoodIdentityDbContext>();    
        await context.Database.EnsureCreatedAsync();    
        var pendingMigration = await context.Database.GetPendingMigrationsAsync();
        var wordHasherService = services.GetRequiredService<IWordHasherService>();    
        
        if (!pendingMigration.Any())
        {
            await context.Database.MigrateAsync();
            
        }
        try
        {

            await DefaultUserSeeder.seedData(context,wordHasherService);


        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        
        
    }

}