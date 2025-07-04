using System.Text;
using Api.JwtConfiguration;
using Identity.Application;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using Shared;
using Shared.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, config) =>
    config.ReadFrom.Configuration(context.Configuration));


builder.Services.AddControllers();
var allAssembly = AppDomain.CurrentDomain.GetAssemblies();

var jwtOption = builder.Configuration.GetSection("Jwt");


// Add services to the container. 
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi(options =>
{
    options.AddDocumentTransformer<BearerSecuritySchemeTransformer>();

});


builder.Services.AddShared(builder.Configuration,allAssembly);
builder.Services.AddIdentityModules(builder.Configuration);
var app = builder.Build();

app.MapOpenApi();
app.UseShared()
    .UseIdentityModule();

app.MapControllers();


app.UseMiddleware<GlobalExceptionHandlingMiddleware>();
app.UseHttpsRedirection();
app.Run();
