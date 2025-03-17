using DotnetMessenger.Web.Features.Chat.CreateChat;
using DotnetMessenger.Web.Features.Chat.CreateChatMessage;
using DotnetMessenger.Web.Features.Chat.DeleteChatMessage;
using DotnetMessenger.Web.Features.Chat.GetChatMessages;
using DotnetMessenger.Web.Features.Chat.UpdateChatMessage;
using Microsoft.AspNetCore.Mvc;

namespace DotnetMessenger.Web.Endpoints.Chat;

public static class ChatEndpoint
{
    public static RouteGroupBuilder MapChatEndpoints(this RouteGroupBuilder mainGroup)
    {
        var group = mainGroup.MapGroup("/chat");

        group.MapPost("", CreateChatFeature);
        group.MapPost("/{chatId:long}/get-chat-messages", GetChatMessages);
        
        group.MapPost("/message", CreateChatMessage);
        group.MapDelete("/message", DeleteChatMessage);
        group.MapPut("/message", UpdateChatMessage);

        return group;
    }
    
    private static async Task<IResult> GetChatMessages(
        [FromServices] GetChatMessagesFeature service,
        [FromRoute] long chatId,
        [FromBody] GetChatMessagesRequest request,
        CancellationToken cancellationToken)
    {
        var result = await service.GetChatMessagesAsync(
            request, 
            chatId,
            cancellationToken);
            
        return Results.Ok(result);
    }
    
    private static async Task<IResult> CreateChatMessage(
        [FromServices] CreateChatMessageFeature service,
        [FromBody] CreateChatMessageRequest request,
        CancellationToken cancellationToken)
    {
        var result = await service.CreateChatMessageAsync(
            request, 
            cancellationToken);
            
        return Results.Ok(result);
    }
    
    private static async Task<IResult> DeleteChatMessage(
        [FromServices] DeleteChatMessageFeature service,
        [FromBody] DeleteChatMessageRequest request,
        CancellationToken cancellationToken)
    {
        await service.DeleteChatMessageAsync(
            request, 
            cancellationToken);
            
        return Results.Ok();
    }
    
    private static async Task<IResult> UpdateChatMessage(
        [FromServices] UpdateChatMessageFeature service,
        [FromBody] UpdateChatMessageRequest request,
        CancellationToken cancellationToken)
    {
        await service.UpdateChatMessageAsync(
            request, 
            cancellationToken);
            
        return Results.Ok();
    }

    private static async Task<IResult> CreateChatFeature(
        [FromServices] CreateChatFeature service,
        [FromBody] CreateChatRequest request,
        CancellationToken cancellationToken)
    {
        var result = await service.CreateChatAsync(request, cancellationToken);
        
        return Results.Ok(result);
    }
}