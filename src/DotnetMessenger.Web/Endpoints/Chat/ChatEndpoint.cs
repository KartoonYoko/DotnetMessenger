﻿using DotnetMessenger.Web.Features.Chat.CreateChat;
using DotnetMessenger.Web.Features.Chat.CreateChatMessage;
using DotnetMessenger.Web.Features.Chat.DeleteChatMessage;
using DotnetMessenger.Web.Features.Chat.GetChatMessages;
using DotnetMessenger.Web.Features.Chat.UpdateChatMessage;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace DotnetMessenger.Web.Endpoints.Chat;

public static class ChatEndpoint
{
    public static RouteGroupBuilder MapChatEndpoints(this RouteGroupBuilder mainGroup)
    {
        var group = mainGroup.MapGroup("/chat");

        group
            .MapPost("", CreateChatFeature)
            .WithSummary("Create a new chat");
        group
            .MapPost("/{chatId:long}/get-chat-messages", GetChatMessages)
            .WithSummary("Get chat messages");
        
        group
            .MapPost("/message", CreateChatMessage)
            .WithSummary("Create a new chat message");
        group
            .MapDelete("/message", DeleteChatMessage)
            .WithSummary("Delete chat message");
        group
            .MapPut("/message", UpdateChatMessage)
            .WithSummary("Update chat message");

        return group;
    }
    
    private static async Task<Ok<GetChatMessagesResponse>> GetChatMessages(
        [FromServices] GetChatMessagesFeature service,
        [FromRoute] long chatId,
        [FromBody] GetChatMessagesRequest request,
        CancellationToken cancellationToken)
    {
        var result = await service.GetChatMessagesAsync(
            request, 
            chatId,
            cancellationToken);
            
        return TypedResults.Ok(result);
    }
    
    private static async Task<Ok<long>> CreateChatMessage(
        [FromServices] CreateChatMessageFeature service,
        [FromBody] CreateChatMessageRequest request,
        CancellationToken cancellationToken)
    {
        var result = await service.CreateChatMessageAsync(
            request, 
            cancellationToken);
            
        return TypedResults.Ok(result);
    }
    
    private static async Task<Ok> DeleteChatMessage(
        [FromServices] DeleteChatMessageFeature service,
        [FromBody] DeleteChatMessageRequest request,
        CancellationToken cancellationToken)
    {
        await service.DeleteChatMessageAsync(
            request, 
            cancellationToken);
            
        return TypedResults.Ok();
    }
    
    private static async Task<Ok> UpdateChatMessage(
        [FromServices] UpdateChatMessageFeature service,
        [FromBody] UpdateChatMessageRequest request,
        CancellationToken cancellationToken)
    {
        await service.UpdateChatMessageAsync(
            request, 
            cancellationToken);
            
        return TypedResults.Ok();
    }

    private static async Task<Ok<long>> CreateChatFeature(
        [FromServices] CreateChatFeature service,
        [FromBody] CreateChatRequest request,
        CancellationToken cancellationToken)
    {
        var result = await service.CreateChatAsync(request, cancellationToken);
        
        return TypedResults.Ok(result);
    }
}