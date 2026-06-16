using Amazon.Lambda.AspNetCoreServer;
using APIService.Services;

var builder = WebApplication.CreateBuilder(args);

#if DEBUG
builder.Configuration.AddJsonFile("appsettings.Debug.json", optional: true, reloadOnChange: true);
#else
builder.Configuration.AddJsonFile("appsettings.Release.json", optional: true, reloadOnChange: true);
#endif

builder.Services.AddSingleton<IApplicationInstanceService, ApplicationInstanceService>();
builder.Services.AddTransient<IRequestOperationService, RequestOperationService>();
builder.Services.AddControllers();
builder.Services.AddAWSLambdaHosting(LambdaEventSource.HttpApi);

var app = builder.Build();

//Needed for health check in AWS Lambda
app.MapGet("/", () => Results.Ok(new
{
    Service = "APIService",
    Status = "Healthy"
}));

app.MapControllers();

app.Run();
