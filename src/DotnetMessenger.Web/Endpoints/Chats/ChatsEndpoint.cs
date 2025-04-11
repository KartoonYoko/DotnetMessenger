using DotnetMessenger.Web.Features.Chats.GetUserChats;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace DotnetMessenger.Web.Endpoints.Chats;

public static class ChatsEndpoint
{
    public static RouteGroupBuilder MapChatsEndpoints(this RouteGroupBuilder mainGroup)
    {
        var group = mainGroup.MapGroup("/chats");

        group
            .MapPost("/get-user-chats", GetUserChats)
            .WithSummary("Get user chats");

        return group;
    }
    
    private static async Task<Ok<GetUserChatsResponse>> GetUserChats(
        [FromServices] GetUserChatsFeature service,
        [FromBody] GetUserChatsRequest request,
        CancellationToken cancellationToken)
    {
        var result = await service.GetUserChatsAsync(
            request, 
            cancellationToken);
            
        return TypedResults.Ok(result);
    }
}