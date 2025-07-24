using MediatR;

namespace Shared.Domain.MediatR;

public interface IQueryHandler<in TQuery, TResult> :
    IRequestHandler<TQuery, TResult> where TQuery : IQuery<TResult>
{ }