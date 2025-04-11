using DotnetMessenger.Web.Common.Services.UserManager;
using DotnetMessenger.Web.Data.Context;
using DotnetMessenger.Web.Features.Chat.GetChatMessages.Errors;
using Microsoft.EntityFrameworkCore;

namespace DotnetMessenger.Web.Features.Chat.GetChatMessages;

public record GetChatMessagesRequest(
    int Skip,
    int Take);

public record GetChatMessagesResponse(List<MessageModel> Messages);

public record MessageModel(
    long MessageId, 
    long UserId,
    DateTime CreatedAt,
    string Text);

public class GetChatMessagesFeature(
    ApplicationDbContext context,
    UserManagerService userManager)
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
        long chatId,
        CancellationToken cancellationToken)
    {
        var hasUserChatAccess = await context
            .ChatUsers
            .Where(x => x.UserId == userManager.GetRequiredUserId())
            .Where(x => x.ChatId == chatId)
            .AnyAsync(cancellationToken);

        if (!hasUserChatAccess)
            throw new UserHasNotAccessToChatException();

        var messages = await context
            .Messages
            .AsNoTracking()
            .Where(x => x.ChatId == chatId)
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
                message.CreatedAt,
                message.Text ?? string.Empty);
            
            messagesModels.Add(messageModel);
        }
        
        return new GetChatMessagesResponse(messagesModels);
    }
}