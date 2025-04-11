using DotnetMessenger.Web.Common.Services.JwtServices;
using DotnetMessenger.Web.Common.Services.PasswordHasher;
using DotnetMessenger.Web.Common.Services.UserManager;

namespace DotnetMessenger.Web.Common.Services;

public static class ConfigureCommonServicesExtension
{
    public static IServiceCollection ConfigureCommonServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddJwtServices(configuration);
        services.AddPasswordHasherServices(configuration);
        services.AddScoped<UserManagerService>();

        return services;
    }
}