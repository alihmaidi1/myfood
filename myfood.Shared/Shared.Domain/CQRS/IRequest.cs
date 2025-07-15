namespace Shared.Domain.CQRS;

public interface IRequest
{
 
    public Guid? RequestId { get; set; }   
}