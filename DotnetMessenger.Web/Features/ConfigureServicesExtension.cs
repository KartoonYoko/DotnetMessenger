using DotnetMessenger.Web.Features.Authentication;
using DotnetMessenger.Web.Features.Chats;
using DotnetMessenger.Web.Features.Message;

namespace DotnetMessenger.Web.Features;

public static class ConfigureServicesExtension
{
    public static IServiceCollection ConfigureFeatureServices(this IServiceCollection services)
    {
        services.AddAuthenticationFeatures();
        services.AddMessageFeatures();
        services.AddChatsFeatures();
        
        return services;
    }
}