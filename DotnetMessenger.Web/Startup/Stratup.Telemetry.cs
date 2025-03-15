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
            .ConfigureResource(resource => resource.AddService(
                serviceName: ServiceName,
                serviceVersion: ServiceVersion));
        
        openTelemetryBuilder.WithTracing(tracing => tracing
                .AddAspNetCoreInstrumentation()
                .AddEntityFrameworkCoreInstrumentation()
                .AddOtlpExporter());
        
        openTelemetryBuilder.WithMetrics(metrics => metrics
                .AddAspNetCoreInstrumentation()
                .AddOtlpExporter());

        builder.Services.AddSingleton<Tracer>(sp =>
        {
            var tracer = sp.GetRequiredService<TracerProvider>();
            
            return tracer.GetTracer(ServiceName, ServiceVersion);
        });
    }
}