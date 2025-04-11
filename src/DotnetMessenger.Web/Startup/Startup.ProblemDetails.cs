namespace DotnetMessenger.Web.Startup;

public static partial class Startup
{
    private static void AddApplicationProblemDetails(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddProblemDetails(x =>
        {
            x.CustomizeProblemDetails = context =>
            {
                context.ProblemDetails.Instance = 
                    $"{context.HttpContext.Request.Method} {context.HttpContext.Request.Path}";
            };
        });
    }
}