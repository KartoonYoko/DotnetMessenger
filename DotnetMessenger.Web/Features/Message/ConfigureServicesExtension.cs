using DotnetMessenger.Web.Features.Message.CreateMessage;

namespace DotnetMessenger.Web.Features.Message;

public static class ConfigureServicesExtension
{
    public static IServiceCollection ConfigureMessageFeatureServices(this IServiceCollection services)
    {
        services.AddScoped<CreateMessageFeature>();
        
        return services;
    }
}