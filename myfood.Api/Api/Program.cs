using System.Text;
using Api.JwtConfiguration;
using Identity.Application;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
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
builder.Services.AddIdentityModules(builder.Configuration);
builder.Services.AddNotificationModule(builder.Configuration);

var app = builder.Build();

app.MapOpenApi();
app.UseShared()
    .UseIdentityModule()
    .UseNotificationModule();

app.MapControllers();


app.UseMiddleware<GlobalExceptionHandlingMiddleware>();
app.UseHttpsRedirection();
app.Run();
