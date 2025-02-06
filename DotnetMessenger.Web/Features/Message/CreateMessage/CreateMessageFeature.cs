using DotnetMessenger.Web.Data.Context;

namespace DotnetMessenger.Web.Features.Message.CreateMessage;

public sealed record CreateMessageRequest(
    long ChatId,
    string Text);

public class CreateMessageFeature(ApplicationDbContext context)
{
    public async Task<long> CreateMessageAsync(
        CreateMessageRequest request,
        CancellationToken cancellationToken)
    {
        // TODO get users
        // var message = new Data.Entities.Message
        // {
        //     
        // };

        throw new NotImplementedException();
    }
}