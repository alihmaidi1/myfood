using Carter;
using Common.File.Command.StartVideoUpload;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Shared.Contract.CQRS;
using Shared.OperationResult;
using Shared.Services.File;

namespace Common.File.Command.CompleteMultipartUpload;

public class CompleteMultipartUploadEndPoint: ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        
        app.MapPost("/files/CompleteMultipartUpload",
                async ([FromBody]  CompleteMultipartUploadRequest request,ICommandHandler<CompleteMultipartUploadRequest> handler,CancellationToken cancellationToken) =>
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