using DotnetMessenger.Web.Endpoints.Authentication;
using DotnetMessenger.Web.Endpoints.Message;
using DotnetMessenger.Web.Endpoints.Messages;

namespace DotnetMessenger.Web.Endpoints;

public static class EndpointsExtension
{
    public static void MapEndpoints(this WebApplication app)
    {
        app.MapGet("/", () => "Hello World!");

        var mainGroup = app.MapGroup("/api");
        
        mainGroup.MapMessageEndpoints();
        mainGroup.MapMessagesEndpoints();
        mainGroup.MapAuthenticationEndpoints();
    }
}