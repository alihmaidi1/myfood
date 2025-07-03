namespace Shared.Contract.CQRS;

public interface ICommand: IRequest
{
    
    
    // public static implicit operator IRequest(ICommand source)
    // {
    //     return source as IRequest;
    //     
    // }
    
}