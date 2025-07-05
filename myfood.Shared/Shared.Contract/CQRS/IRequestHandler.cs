
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Shared.Contract.CQRS;

public interface IRequestHandler<in TCommand> where TCommand : IRequest
{
    public Task<IResult> Handle(TCommand request, CancellationToken cancellationToken);
    
 
    
}