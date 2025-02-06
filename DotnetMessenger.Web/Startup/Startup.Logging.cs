using Serilog;

namespace DotnetMessenger.Web.Startup;

public static partial class Startup
{
    private static void AddLogging(this IServiceCollection services)
    {
        services.AddSerilog();
    }
}