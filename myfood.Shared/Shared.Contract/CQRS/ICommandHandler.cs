namespace Shared.Contract.CQRS;

public interface ICommandHandler<in TCommand>:IRequestHandler<TCommand> where TCommand : ICommand
{
    
    


}