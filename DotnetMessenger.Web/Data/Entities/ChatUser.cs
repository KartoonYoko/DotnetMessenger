namespace DotnetMessenger.Web.Data.Entities;

public class ChatUser
{
    public long Id { get; set; }
    public long ChatId { get; set; }
    public Chat? Chat { get; set; }
    public long UserId { get; set; }
    public User? User { get; set; }
}