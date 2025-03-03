namespace DotnetMessenger.Web.Startup;

public static partial class Startup
{
    private static void AddOpenApiConfiguration(
        this IServiceCollection services)
    {
        services.AddOpenApi();
    }

    private static void UseOpenApi(this WebApplication app)
    {
        app.MapOpenApi();
        
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/openapi/v1.json", "v1");
        });
    }
}