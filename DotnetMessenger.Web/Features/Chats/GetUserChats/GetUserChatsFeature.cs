using DotnetMessenger.Web.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace DotnetMessenger.Web.Features.Chats.GetUserChats;

public record GetUserChatsRequest(
    long UserId,
    int Skip,
    int Limit);

public record GetUserChatsResponse(List<ChatModel> Chats);

public class ChatModel(long Id, string Title);

public class GetUserChatsFeature(
    ApplicationDbContext context)
{
    public async Task<GetUserChatsResponse> GetUserChatsAsync(
        GetUserChatsRequest request,
        CancellationToken cancellationToken)
    {
        var chats = await context
            .ChatUsers
            .AsNoTracking()
            .Include(x => x.Chat)
            .Where(x => x.UserId == request.UserId)
            .OrderBy(x => x.ChatId)
            .Skip(request.Skip)
            .Take(request.Limit)
            .Select(x => new ChatModel(x.ChatId, x.Chat!.Title))
            .ToListAsync(cancellationToken);
        
        return new GetUserChatsResponse(chats);
    }
}