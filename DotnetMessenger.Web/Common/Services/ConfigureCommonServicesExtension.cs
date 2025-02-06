using DotnetMessenger.Web.Common.Services.JwtServices;
using DotnetMessenger.Web.Common.Services.PasswordHasher;

namespace DotnetMessenger.Web.Common.Services;

public static class ConfigureCommonServicesExtension
{
    public static IServiceCollection ConfigureCommonServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddJwtServices(configuration);
        services.AddPasswordHasherServices(configuration);

        return services;
    }
}