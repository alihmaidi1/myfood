var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, config) =>
    config.ReadFrom.Configuration(context.Configuration));

var allAssembly = AppDomain.CurrentDomain.GetAssemblies();

// Add services to the container. 
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi



#region Shared

builder.Services.AddInfrastructure(builder.Configuration,ModuleExtension.GetModuleAssemblyTypes().Keys.ToList());
builder.Services.AddScoped<GlobalExceptionHandlingMiddleware>();
builder.Services.AddOpenApi(options =>
{
    options.AddDocumentTransformer<BearerSecuritySchemeTransformer>();

});




builder.Services.AddApplication(ModuleExtension.GetModuleAssemblyTypes(),AppDomain.CurrentDomain.GetAssemblies().ToList());
builder.Services.AddMassTransitWithAssemblies(builder.Configuration, allAssembly);

#endregion



#region Modulars

builder.Services
    .AddIdentityApplicationModules(builder.Configuration)
    .AddIdentityInfrastructureModule(builder.Configuration)
    .AddCommonModule()
    .AddNotificationModule(builder.Configuration);


#endregion


var app = builder.Build();

app.MapOpenApi();
app.UseInfrastructure()
    .UseApplication()
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