namespace DotnetMessenger.Web.Common.Services.JwtServices;

public static class ConfigureJwtServicesExtension
{
    public static IServiceCollection AddJwtServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddScoped<JwtHandlerService>();
        
        services.Configure<JwtHandlerOptions>(
            configuration.GetSection("Jwt"));
        
        return services;
    }
}