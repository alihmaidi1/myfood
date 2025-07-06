using Carter;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Shared.Contract.CQRS;
using Shared.OperationResult;

namespace Common.File.Command.UploadImage;

public class UploadImageEndPoint: ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/images/uploadPresignedUrl",
                async ([FromBody]  UploadImageRequest request,ICommandHandler<UploadImageRequest> handler,CancellationToken cancellationToken) =>
                {
                    
                    var result=await handler.Handle(request, cancellationToken);
                    return  result;
                })
            .Produces<TResult<string>>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("get PresignedUrl")
            
            .WithDescription("get PresignedUrl");

    }
}