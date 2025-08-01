using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Shared.Infrastructure.Database;
using Shared.Infrastructure.Messages.Inbox;
using Shared.Infrastructure.Messages.Outbox;

namespace Notification;

public class myFoodNotificationDbContext: DbContext
{
    
    
    public myFoodNotificationDbContext(DbContextOptions<myFoodNotificationDbContext> option) : base(option)
    {


    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.HasDefaultSchema(Schemas.Notification);
        
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        builder.ApplyConfiguration(new OutboxMessageConfiguration());
        builder.ApplyConfiguration(new OutboxMessageConsumerConfiguration());
        builder.ApplyConfiguration(new InboxMessageConfiguration());
        builder.ApplyConfiguration(new InboxMessageConsumerConfiguration());
        base.OnModelCreating(builder);

    }
    
    // public DbSet<ArchiveRecord>  ArchiveRecords { get; init; }
    // public DbSet<OutboxMessage> OutboxMessages { get; init; }
    //
    // public DbSet<RefreshToken> RefreshTokens { get; init; }

}