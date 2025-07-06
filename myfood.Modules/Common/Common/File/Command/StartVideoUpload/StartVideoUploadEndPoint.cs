using Carter;
using Common.File.Command.UploadImage;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Shared.Contract.CQRS;
using Shared.OperationResult;
using Shared.Services.File;

namespace Common.File.Command.StartVideoUpload;

public class StartVideoUploadEndPoint: ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        
        app.MapPost("/files/startVideoUpload",
                async ([FromBody]  StartVideoUploadRequest request,ICommandHandler<StartVideoUploadRequest> handler,CancellationToken cancellationToken) =>
                {
                    
                    var result=await handler.Handle(request, cancellationToken);
                    return  result;
                })
            .Produces<TResult<ChunkUploadResponse>>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Start Video Upload")
            .WithDescription("Start Video Upload");
    }
}