using DotnetMessenger.Web.Endpoints.Authentication;
using DotnetMessenger.Web.Endpoints.Chat;
using DotnetMessenger.Web.Endpoints.Chats;
using DotnetMessenger.Web.Endpoints.Users;
using Microsoft.AspNetCore.HttpLogging;
using OpenTelemetry.Trace;

namespace DotnetMessenger.Web.Endpoints;

public static class EndpointsExtension
{
    public static void MapEndpoints(this WebApplication app)
    {
        app.MapGet("/", (Tracer tracer, ILogger<Program> logger) =>
        {
            using var span = tracer.StartActiveSpan(
                "hello-span", 
                initialAttributes: new SpanAttributes([
                    new KeyValuePair<string, object?>("init-attribute-1", "value-1"),
                ]));
            
            logger.LogInformation("Some log message");
            
            span.SetAttribute("attribute-2", "value-2");
            
            span.AddEvent("hello-event");
            
            span.SetAttribute("attribute-3", "value-3");
            
            return "Hello World";
        });

        var mainGroup = app.MapGroup("/api");

        mainGroup.MapAuthenticationEndpoints();
        mainGroup.MapUsersEndpoints().RequireAuthorization();
        mainGroup.MapChatsEndpoints().RequireAuthorization();
        mainGroup.MapChatEndpoints().RequireAuthorization();
    }
}