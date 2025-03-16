using DotnetMessenger.Web.Features.Authentication;
using DotnetMessenger.Web.Features.Chat;
using DotnetMessenger.Web.Features.Chats;
using DotnetMessenger.Web.Features.Users;

namespace DotnetMessenger.Web.Features;

public static class ConfigureServicesExtension
{
    public static IServiceCollection ConfigureFeatureServices(this IServiceCollection services)
    {
        services.AddAuthenticationFeatures();
        services.AddChatsFeatures();
        services.AddChatFeatures();
        services.AddUsersFeatures();
        
        return services;
    }
}