using System.Reflection;
using Identity.Domain.Security;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using myfood.Messages.Outbox;
using Shared.Services.Archive;

namespace Identity.infrastructure;

public class myFoodDbContext: IdentityDbContext<User,Role,Guid>
{
    
    
    public myFoodDbContext(DbContextOptions<myFoodDbContext> option) : base(option)
    {


    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.HasDefaultSchema("Identity");
        
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(builder);

    }
    
    public DbSet<ArchiveRecord>  ArchiveRecords { get; init; }
    public DbSet<OutboxMessage> OutboxMessages { get; init; }
    public DbSet<OutboxConsumer>  OutboxConsumers { get; init; }
    
    public DbSet<RefreshToken> RefreshTokens { get; init; }

}