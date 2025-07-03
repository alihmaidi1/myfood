
using Microsoft.AspNetCore.Mvc;

namespace Shared.Contract.CQRS;

public interface IRequestHandler<in TCommand> where TCommand : IRequest
{
    public Task<JsonResult> Handle(TCommand request, CancellationToken cancellationToken);
    
 
    
}