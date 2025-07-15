namespace Shared.Domain.CQRS;

public interface ICommandHandler<in TCommand>:IRequestHandler<TCommand> where TCommand : ICommand
{
    
    


}