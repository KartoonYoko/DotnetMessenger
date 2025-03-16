using DotnetMessenger.Web.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace DotnetMessenger.Web.Features.Chat.DeleteChatMessage;

public record DeleteChatMessageRequest(long MessageId);

public class DeleteChatMessageFeature(ApplicationDbContext context)
{
    public async Task DeleteChatMessageAsync(
        DeleteChatMessageRequest request,
        CancellationToken cancellationToken)
    {
        await context
            .Messages
            .Where(x => x.Id == request.MessageId)
            .ExecuteDeleteAsync(cancellationToken);
    }
}