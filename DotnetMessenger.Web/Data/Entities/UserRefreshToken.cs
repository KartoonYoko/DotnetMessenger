namespace DotnetMessenger.Web.Data.Entities;

public class UserRefreshToken
{
    public long Id { get; set; }
    
    public required string Token { get; set; }
    
    public DateTime CreatedAt { get; set; }
    
    public DateTime Expires { get; set; }
    
    public long UserId { get; set; }
    
    public User? User { get; set; }
}