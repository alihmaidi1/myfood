using Carter;
using Common.File.Command.CompleteMultipartUpload;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Shared.Contract.CQRS;
using Shared.OperationResult;

namespace Common.File.Command.CancelUpload;

public class CancelUploadEndPoint: ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        
        
        app.MapPost("/files/CancelUpload",
                async ([FromBody]  CancelUploadRequest request,ICommandHandler<CancelUploadRequest> handler,CancellationToken cancellationToken) =>
                {
                    
                    var result=await handler.Handle(request, cancellationToken);
                    return  result;
                })
            .Produces<TResult<bool>>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("completedMultipartUpload")
            .WithDescription("completedMultipartUpload");
    }
}