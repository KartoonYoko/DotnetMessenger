using DotnetMessenger.Web.Common.Services.UserManager;
using DotnetMessenger.Web.Data.Context;
using DotnetMessenger.Web.Data.Entities;

namespace DotnetMessenger.Web.Features.Chat.CreateChat;

public record CreateChatRequest(
    string ChatTitle,
    long ChatWithUserId);

public class CreateChatFeature(
    ApplicationDbContext context,
    UserManagerService userManagerService)
{
    public async Task<long> CreateChatAsync(
        CreateChatRequest request,
        CancellationToken cancellationToken)
    {
        var newChat = new Data.Entities.Chat
        {
            Title = request.ChatTitle,
        };
        
        await context.Chats.AddAsync(newChat, cancellationToken);

        var userWithChat = new ChatUser
        {
            Chat = newChat,
            UserId = request.ChatWithUserId
        };
        
        await context.ChatUsers.AddAsync(userWithChat, cancellationToken);
        
        var userCreateChat = new ChatUser
        {
            Chat = newChat,
            UserId = userManagerService.GetRequiredUserId()
        };
        
        await context.ChatUsers.AddAsync(userCreateChat, cancellationToken);
        
        await context.SaveChangesAsync(cancellationToken);
        
        return newChat.Id;
    }
}