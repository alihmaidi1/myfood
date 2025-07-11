
using Api.JwtConfiguration;

using Identity.Application;
using Identity.infrastructure;
using myfood.Messages.Extensions;
using Notification;
using Serilog;
using Shared;
using Shared.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, config) =>
    config.ReadFrom.Configuration(context.Configuration));

builder.Services.AddControllers();
var allAssembly = AppDomain.CurrentDomain.GetAssemblies();



// Add services to the container. 
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi(options =>
{
    options.AddDocumentTransformer<BearerSecuritySchemeTransformer>();

});


builder.Services.AddShared(builder.Configuration,allAssembly);
builder.Services.AddMassTransitWithAssemblies(builder.Configuration, allAssembly);

builder.Services.AddIdentityApplicationModules(builder.Configuration);
builder.Services.AddIdentityInfrastructureModule(builder.Configuration);

builder.Services.AddNotificationModule(builder.Configuration);

var app = builder.Build();

app.MapOpenApi();
app.UseShared()
    .UseIdentityApplicationModule()
    .UseIdentityInfrastructureModule()
    .UseNotificationModule();

app.MapControllers();


app.UseMiddleware<GlobalExceptionHandlingMiddleware>();
app.UseHttpsRedirection();
app.Run();

public partial class Program
{
    
}