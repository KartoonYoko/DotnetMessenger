using DotnetMessenger.Web.Data.Context;
using DotnetMessenger.Web.Data.Entities;
using OpenTelemetry.Trace;

namespace DotnetMessenger.Web.Features.Chat.CreateChatMessage;

public record CreateChatMessageRequest(
    long ChatId,
    long UserId,
    string Text);
    
public class CreateChatMessageFeature(
    ApplicationDbContext context,
    Tracer tracer)
{
    public async Task<long> CreateChatMessageAsync(
        CreateChatMessageRequest request,
        CancellationToken cancellationToken)
    {
        using var span = tracer.StartActiveSpan(
            "CreateChatMessageFeature.CreateChatMessageAsync",
            initialAttributes: new SpanAttributes([
                new KeyValuePair<string, object?>("chat.id", request.ChatId),
                new KeyValuePair<string, object?>("chat.message.text", request.Text)
            ]));
        
        var message = new Message
        {
            ChatId = request.ChatId,
            Text = request.Text,
            CreatedAt = DateTime.UtcNow,
            CreatedBy = request.UserId,
        };
        
        context.Messages.Add(message);
        
        await context.SaveChangesAsync(cancellationToken);
        
        span.AddEvent("Added message");
        
        return message.Id;        
    }
}