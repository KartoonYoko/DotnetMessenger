namespace DotnetMessenger.Web.Endpoints.Messages;

public static class MessagesEndpoint
{
    public static void MapMessagesEndpoints(this RouteGroupBuilder mainGroup)
    {
        var group = mainGroup.MapGroup("/messages");

        group.MapGet("", GetMessages);
    }
    
    private static async Task<IResult> GetMessages(
        CancellationToken cancellationToken)
    {
        return Results.Ok();
    }
}