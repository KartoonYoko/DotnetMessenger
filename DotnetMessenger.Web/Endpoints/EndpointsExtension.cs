using DotnetMessenger.Web.Endpoints.Authentication;
using DotnetMessenger.Web.Endpoints.Chat;
using DotnetMessenger.Web.Endpoints.Chats;
using DotnetMessenger.Web.Endpoints.Users;

namespace DotnetMessenger.Web.Endpoints;

public static class EndpointsExtension
{
    public static void MapEndpoints(this WebApplication app)
    {
        app.MapGet("/", () => "Hello World!");

        var mainGroup = app.MapGroup("/api");

        mainGroup.MapUsersEndpoints();
        mainGroup.MapChatsEndpoints();
        mainGroup.MapChatEndpoints();
        mainGroup.MapAuthenticationEndpoints();
    }
}