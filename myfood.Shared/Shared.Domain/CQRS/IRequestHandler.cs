using Microsoft.AspNetCore.Http;

namespace Shared.Domain.CQRS;

public interface IRequestHandler<in TCommand> where TCommand : IRequest
{
    public Task<IResult> Handle(TCommand request, CancellationToken cancellationToken);
    
 
    
}