namespace DotnetMessenger.Web.Data.Entities;

public class User
{
    public long Id { get; set; }
    public required string Login { get; set; }
    public required string Password { get; set; }
}