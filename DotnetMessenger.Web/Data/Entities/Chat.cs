namespace DotnetMessenger.Web.Data.Entities;

public class Chat
{
    public long Id { get; set; }
    public required string Title { get; set; }
    public List<Message>? Messages { get; set; }
}