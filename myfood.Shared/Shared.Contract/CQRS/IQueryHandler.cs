namespace Shared.Contract.CQRS;

public interface IQueryHandler<in TCommand>: IRequestHandler<TCommand> where TCommand : IQuery
{
    
}