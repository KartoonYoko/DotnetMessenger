using DotnetMessenger.Web.Features.Users.GetUsersPage;

namespace DotnetMessenger.Web.Features.Users;

public static class ConfigureServicesExtension
{
    public static IServiceCollection AddUsersFeatures(this IServiceCollection services)
    {
        services.AddScoped<GetUsersPageFeature>();
        
        return services;
    }
}