using DotnetMessenger.Web.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace DotnetMessenger.Web.Features.Chat.UpdateChatMessage;

public record UpdateChatMessageRequest(
    long MessageId,
    string NewText);

public class UpdateChatMessageFeature(ApplicationDbContext context)
{
    public async Task UpdateChatMessageAsync(
        UpdateChatMessageRequest request,
        CancellationToken cancellationToken)
    {
        await context
            .Messages
            .Where(x => x.Id == request.MessageId)
            .ExecuteUpdateAsync(
                x => x.SetProperty(p => p.Text, request.NewText), 
                cancellationToken);
    }
}