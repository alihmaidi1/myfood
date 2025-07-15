namespace Shared.Domain.CQRS;

public interface IQueryHandler<in TCommand>: IRequestHandler<TCommand> where TCommand : IQuery
{
    
}