using DotnetMessenger.Web.Startup;
using Serilog;
using Serilog.Sinks.OpenTelemetry;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console(outputTemplate:
        "[{Timestamp:HH:mm:ss} {Level:u3}] {TraceId} {SpanId} {Message:lj}{NewLine}{Exception}")
    .WriteTo.OpenTelemetry(x =>  
    {  
        const string serviceName = "dotnet.messenger.web";
        
        // x.Endpoint = "http://localhost:5341/ingest/otlp/v1/logs";
        x.Endpoint = "http://localhost:4318";
        x.Protocol = OtlpProtocol.HttpProtobuf;
        x.Headers = new Dictionary<string, string>()
        {
            ["X-Seq-ApiKey"] = "WkyyZupSCL7lwQCrkjfM"
        };
        x.ResourceAttributes = new Dictionary<string, object>()
        {
            ["service.name"] = serviceName,
            ["deployment.environment"] = "development"
        };
    })
    .CreateLogger();

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.ConfigureServices();

    var app = builder.Build();

    await app.ConfigureAsync();

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}