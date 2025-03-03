using DotnetMessenger.Web.Features.Authentication;
using DotnetMessenger.Web.Features.Chats;

namespace DotnetMessenger.Web.Features;

public static class ConfigureServicesExtension
{
    public static IServiceCollection ConfigureFeatureServices(this IServiceCollection services)
    {
        services.AddAuthenticationFeatures();
        services.AddChatsFeatures();
        
        return services;
    }
}