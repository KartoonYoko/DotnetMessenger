namespace DotnetMessenger.Web.Common.Services.PasswordHasher;

public static class ConfigurePasswordHasherServicesExtension
{
    public static IServiceCollection AddPasswordHasherServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<PasswordHasherOptions>(
            configuration.GetSection("PasswordHasher"));

        services.AddScoped<PasswordHasherService>();
        
        return services;
    }
}