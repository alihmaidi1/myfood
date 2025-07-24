using MediatR;

namespace Shared.Domain.MediatR;


public interface IQuery<out TResult> : IRequest<TResult> { }
