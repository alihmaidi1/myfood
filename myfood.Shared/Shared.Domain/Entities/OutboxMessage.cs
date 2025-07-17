namespace Shared.Domain.Entities;

public sealed class OutboxMessage
{
    public Guid Id { get; set; }
    public string Type { get; set; }
    public string Content { get; set; }
    
    public DateTime CreatedOn { get; set; }
    
    public DateTime? ProcessedOn { get; set; }
    public string? ErrorMessage { get; set; }
    
    
}