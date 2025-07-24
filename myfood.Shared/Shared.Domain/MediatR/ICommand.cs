using MediatR;

namespace Shared.Domain.MediatR;

public interface ICommand<out TResult> : IRequest<TResult>
{
}

public interface ICommand : IRequest
{
    
}
