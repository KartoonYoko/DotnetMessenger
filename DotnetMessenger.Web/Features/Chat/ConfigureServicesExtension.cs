using DotnetMessenger.Web.Features.Chat.CreateChat;
using DotnetMessenger.Web.Features.Chat.CreateChatMessage;
using DotnetMessenger.Web.Features.Chat.DeleteChatMessage;
using DotnetMessenger.Web.Features.Chat.GetChatMessages;
using DotnetMessenger.Web.Features.Chat.UpdateChatMessage;

namespace DotnetMessenger.Web.Features.Chat;

public static class ConfigureServicesExtension
{
    public static IServiceCollection AddChatFeatures(this IServiceCollection services)
    {
        services.AddScoped<CreateChatFeature>();
        services.AddScoped<CreateChatMessageFeature>();
        services.AddScoped<DeleteChatMessageFeature>();
        services.AddScoped<GetChatMessagesFeature>();
        services.AddScoped<UpdateChatMessageFeature>();
        
        return services;
    }
}