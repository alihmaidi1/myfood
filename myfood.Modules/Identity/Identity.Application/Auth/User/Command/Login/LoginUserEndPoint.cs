using Carter;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Shared.Contract.CQRS;

namespace Identity.Application.Auth.User.Command.Login;

public class LoginUserEndPoint: ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/users/login", 
                async ([FromBody]  LoginUserRequest request,ICommandHandler<LoginUserRequest> handler,CancellationToken cancellationToken) =>
                {
                    var result=await handler.Handle(request, cancellationToken);
                    return result.Value;
                })
            .Produces<string>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Add Item Into Basket")
            .WithDescription("Add Item Into Basket");
    }
}