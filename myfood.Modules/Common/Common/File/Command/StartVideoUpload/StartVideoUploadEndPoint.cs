using Carter;
using Common.File.Command.UploadImage;
using Mapster;
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
                async ([FromBody]  StartVideoUploadRequest request,[FromHeader]Guid RequestId,ICommandHandler<StartVideoUploadCommand> handler,CancellationToken cancellationToken) =>
                {

                    var command = request.Adapt<StartVideoUploadCommand>(); 
                    command.RequestId=RequestId;
                    var result=await handler.Handle(command, cancellationToken);
                    return  result;
                })
            .Produces<TResult<ChunkUploadResponse>>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Start Video Upload")
            .WithTags("Files")
            
            .WithDescription("Start Video Upload");
    }
}