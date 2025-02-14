using DotnetMessenger.Web.Features.Chats.GetUserChats;

namespace DotnetMessenger.Web.Features.Chats;

public static class ConfigureServicesExtension
{
    public static IServiceCollection AddChatsFeatures(this IServiceCollection services)
    {
        services.AddScoped<GetUserChatsFeature>();
        
        return services;
    }
}