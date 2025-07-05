using System.Data;
using Carter;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Shared.Authorization;
using Shared.Contract.CQRS;
using Shared.OperationResult;

namespace Identity.Application.Auth.User.Command.ChangePassword;

public class ChangePasswordEndPoint: ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        
        app.MapPost("/users/changePassword",
                async ([FromBody]  ChangePasswordRequest request,ICommandHandler<ChangePasswordRequest> handler,CancellationToken cancellationToken) =>
                {
                    
                    var result=await handler.Handle(request, cancellationToken);
                    // Results.Ok(result);
                    return  result;
                })
            .Produces<TResult<bool>>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .RequireAuthorization()
            .WithSummary("change user password")
            .WithDescription("change user password");
    
    }
}