using DotnetMessenger.Web.Common.Services;
using DotnetMessenger.Web.Data.Context;
using DotnetMessenger.Web.Endpoints;
using DotnetMessenger.Web.Features;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.HttpLogging;

namespace DotnetMessenger.Web.Startup;

public static partial class Startup
{
    private const string ServiceName = "DotnetMessenger.Web";

    private const string ServiceVersion = "0.0.1";
    
    private const string SourceName = "Source.DotnetMessenger.Web";
    
    public static void ConfigureServices(this WebApplicationBuilder builder)
    {
        var configuration = builder.Configuration;
        var services = builder.Services;

        builder.AddTelemetry();

        services.AddProblemDetails(options =>
        {
            options.CustomizeProblemDetails = context =>
            {
                context.ProblemDetails.Extensions.TryAdd("requestId", context.HttpContext.TraceIdentifier);
                
                var activity = context.HttpContext.Features.Get<IHttpActivityFeature>()?.Activity;
                context.ProblemDetails.Extensions.TryAdd("traceId", activity?.Id);
            };
        });
        services.AddHttpLogging(x =>
        {
            x.LoggingFields = HttpLoggingFields.Duration 
                              | HttpLoggingFields.RequestMethod 
                              | HttpLoggingFields.RequestPath
                              | HttpLoggingFields.RequestProtocol
                              | HttpLoggingFields.ResponseStatusCode;
        });
        services.AddHttpContextAccessor();
        services.AddDatabase(configuration);
        services.AddAuthenticationAndAuthorization(configuration);
        services.AddOpenApiConfiguration();
        services.ConfigureFeatureServices();
        services.ConfigureCommonServices(configuration);
    }

    public static async Task ConfigureAsync(this WebApplication app)
    {
        await app.InitialiseDatabaseAsync();
        app.UseHttpLogging();
        app.UseAuthenticationAndAuthorization();
        app.UseOpenApi();
        app.MapEndpoints();
    }
    
    private static string ValidateConfiguration(this IConfiguration configuration, string configurationPath)
    {
        var result = configuration[configurationPath];
        
        if (string.IsNullOrEmpty(result))
            throw new Exception();

        return result;
    }
}