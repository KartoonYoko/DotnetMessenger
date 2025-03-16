using DotnetMessenger.Web.Data.Context;
using DotnetMessenger.Web.Data.Entities;

namespace DotnetMessenger.Web.Features.Chat.CreateChatMessage;

public record CreateChatMessageRequest(
    long ChatId,
    long UserId,
    string Text);
    
public class CreateChatMessageFeature(ApplicationDbContext context)
{
    public async Task<long> CreateChatMessageAsync(
        CreateChatMessageRequest request,
        CancellationToken cancellationToken)
    {
        var message = new Message
        {
            ChatId = request.ChatId,
            Text = request.Text,
            CreatedAt = DateTime.UtcNow,
            CreatedBy = request.UserId,
        };
        
        context.Messages.Add(message);
        
        await context.SaveChangesAsync(cancellationToken);
        
        return message.Id;        
    }
}