namespace Shared.Contract.CQRS;

public interface IRequest
{
 
    public Guid? RequestId { get; set; }   
}