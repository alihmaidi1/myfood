using System.Reflection;
using Identity.Domain.Security;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Shared.Infrastructure.Configurations;
using Shared.Infrastructure.Database;

namespace Identity.infrastructure;

public class myFoodIdentityDbContext: IdentityDbContext<User,Role,Guid>
{
    
    
    public myFoodIdentityDbContext(DbContextOptions<myFoodIdentityDbContext> option) : base(option)
    {


    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.HasDefaultSchema(Schemas.Identity);
        
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        builder.ApplyConfiguration(new OutboxMessageConfiguration());
        builder.ApplyConfiguration(new OutboxMessageConsumerConfiguration());

        base.OnModelCreating(builder);

    }
    
    // public DbSet<ArchiveRecord>  ArchiveRecords { get; init; }
    // public DbSet<OutboxMessage> OutboxMessages { get; init; }
    
    public DbSet<RefreshToken> RefreshTokens { get; init; }

}