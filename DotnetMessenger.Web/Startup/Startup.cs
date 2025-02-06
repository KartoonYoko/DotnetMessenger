using DotnetMessenger.Web.Common.Services;
using DotnetMessenger.Web.Endpoints;
using DotnetMessenger.Web.Features;

namespace DotnetMessenger.Web.Startup;

public static partial class Startup
{
    public static void ConfigureServices(this WebApplicationBuilder builder)
    {
        var configuration = builder.Configuration;
        var services = builder.Services;
        
        services.AddDatabase(configuration);
        services.AddAuthenticationAndAuthorization(configuration);
        services.AddLogging();
        services.ConfigureFeatureServices();
        services.ConfigureCommonServices(configuration);
    }

    public static void Configure(this WebApplication app)
    {
        app.UseAuthenticationAndAuthorization();
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