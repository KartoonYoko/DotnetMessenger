using OpenTelemetry.Exporter;
using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.OpenTelemetry;

namespace DotnetMessenger.Web.Startup;

public static partial class Startup
{
    private static void AddTelemetry(this WebApplicationBuilder builder)
    {
        builder.Logging.AddOpenTelemetry(options =>
        {
            options
                .SetResourceBuilder(
                    ResourceBuilder.CreateDefault()
                        .AddService(
                            serviceName: ServiceName,
                            serviceVersion: ServiceVersion))
                .AddOtlpExporter();
        });
        
        var openTelemetryBuilder = builder.Services.AddOpenTelemetry();
        
        openTelemetryBuilder
            .WithTracing(tcb =>
            {
                tcb
                    .AddSource(ServiceName)
                    .SetResourceBuilder(
                        ResourceBuilder.CreateDefault()
                            .AddService(serviceName: ServiceName, serviceVersion: ServiceVersion))
                    .AddAspNetCoreInstrumentation()
                    .AddOtlpExporter();
            })
            .WithMetrics(metrics => metrics
                .AddAspNetCoreInstrumentation()
                .AddOtlpExporter());

        builder.Services.AddSingleton(TracerProvider.Default.GetTracer(ServiceName, ServiceVersion));
    }
}