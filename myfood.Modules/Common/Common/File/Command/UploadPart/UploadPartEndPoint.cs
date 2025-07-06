using Carter;
using Common.File.Command.StartVideoUpload;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Shared.Contract.CQRS;
using Shared.OperationResult;
using Shared.Services.File;

namespace Common.File.Command.UploadPart;

public class UploadPartEndPoint: ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        
        app.MapPost("/files/uploadPart",
                async ([FromForm]UploadPartRequest request,ICommandHandler<UploadPartRequest> handler,CancellationToken cancellationToken) =>
                {
                    
                    var result=await handler.Handle(request, cancellationToken);
                    return  result;
                })
            .Produces<TResult<string>>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .DisableAntiforgery()
            .WithSummary("Upload part")
            .WithDescription("Upload part");
    }
}