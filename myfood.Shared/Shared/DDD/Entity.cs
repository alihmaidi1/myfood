namespace Shared.DDD;

public class Entity: IEntity
{
    public Guid Id { get; set; }
    public string CreatedBy { get; set; } = "";
    public DateTime CreatedAt { get; set; }=DateTime.Now;
    public string LastModifiedBy { get; set; } = "";
    public DateTime LastModified { get; set; }=DateTime.Now;
}