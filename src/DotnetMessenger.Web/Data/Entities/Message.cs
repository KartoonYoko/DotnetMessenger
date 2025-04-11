using DotnetMessenger.Web.Data.Abstractions;

namespace DotnetMessenger.Web.Data.Entities;

public class Message : AuditableEntityBase
{
    public long Id { get; set; }
    public string? Text { get; set; }
    public long ChatId { get; set; }
    public Chat? Chat { get; set; }
}