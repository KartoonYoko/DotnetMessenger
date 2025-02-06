namespace DotnetMessenger.Web.Endpoints.Message;

public static class MessageEndpoint
{
    public static void MapMessageEndpoints(this RouteGroupBuilder mainGroup)
    {
        var group = mainGroup.MapGroup("/message");

        group.MapPost("", CreateMessage);
        group.MapPut("", UpdateMessage);
        group.MapDelete("", DeleteMessage);
    }

    private static async Task CreateMessage(
        CancellationToken cancellationToken)
    {
    }
    
    private static async Task UpdateMessage(
        CancellationToken cancellationToken)
    {
    }
    
    private static async Task DeleteMessage(
        CancellationToken cancellationToken)
    {
    }
}