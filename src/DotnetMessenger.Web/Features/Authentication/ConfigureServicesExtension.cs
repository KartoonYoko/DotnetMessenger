using DotnetMessenger.Web.Features.Authentication.Login;
using DotnetMessenger.Web.Features.Authentication.Logout;
using DotnetMessenger.Web.Features.Authentication.RefreshTokens;
using DotnetMessenger.Web.Features.Authentication.Register;

namespace DotnetMessenger.Web.Features.Authentication;

public static class ConfigureServicesExtension
{
    public static IServiceCollection AddAuthenticationFeatures(this IServiceCollection services)
    {
        services.AddScoped<RegisterFeature>();
        services.AddScoped<LoginFeature>();
        services.AddScoped<LogoutFeature>();
        services.AddScoped<RefreshTokensFeature>();
        
        return services;
    }
}