using DotnetMessenger.Web.Data.Context;
using DotnetMessenger.Web.Features.Chat.GetChatMessages.Errors;
using Microsoft.EntityFrameworkCore;

namespace DotnetMessenger.Web.Features.Chat.GetChatMessages;

public record GetChatMessagesRequest(
    long UserId, 
    long ChatId,
    int Skip,
    int Take);

public record GetChatMessagesResponse(List<MessageModel> Messages);

public record MessageModel(
    long MessageId, 
    long UserId,
    string Text);

public class GetChatMessagesFeature(ApplicationDbContext context)
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="UserHasNotAccessToChatException"></exception>
    public async Task<GetChatMessagesResponse> GetChatMessagesAsync(
        GetChatMessagesRequest request,
        CancellationToken cancellationToken)
    {
        var hasUserChatAccess = await context
            .ChatUsers
            .Where(x => x.UserId == request.UserId)
            .Where(x => x.ChatId == request.ChatId)
            .AnyAsync(cancellationToken);

        if (!hasUserChatAccess)
            throw new UserHasNotAccessToChatException();

        var messages = await context
            .Messages
            .AsNoTracking()
            .Where(x => x.ChatId == request.ChatId)
            .OrderByDescending(x => x.CreatedAt)
            .Skip(request.Skip)
            .Take(request.Take)
            .ToListAsync(cancellationToken);

        var messagesModels = new List<MessageModel>(messages.Count);
        
        foreach (var message in messages)
        {
            var messageModel = new MessageModel(
                message.Id,
                message.CreatedBy,
                message.Text ?? string.Empty);
            
            messagesModels.Add(messageModel);
        }
        
        return new GetChatMessagesResponse(messagesModels);
    }
}