using System.Text.Json;
using Microsoft.EntityFrameworkCore;

namespace myfood.Messages.Outbox;

public static class OutboxExtensions
{
    public static async Task InsertOutboxMessage<T>(DbSet<OutboxMessage> dbSet,T message)
    {

        var outboxMessage = new OutboxMessage()
        {
            Id = Guid.NewGuid(),
            Type = message.GetType().AssemblyQualifiedName,
            Content = JsonSerializer.Serialize(message),
            CreatedOn = DateTime.UtcNow


        };
        dbSet.Add(outboxMessage);
    }
    
}