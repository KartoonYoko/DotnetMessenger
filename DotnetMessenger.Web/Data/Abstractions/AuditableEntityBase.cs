using DotnetMessenger.Web.Data.Entities;

namespace DotnetMessenger.Web.Data.Abstractions;

public abstract class AuditableEntityBase
{
    public long CreatedBy { get; set; }
    public required User CreatedByUser { get; set; }
    public required DateTime CreatedAt { get; set; }
    public long? UpdateBy { get; set; }
    public User? UpdatedByUser { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public long? DeletedBy { get; set; }
    public User? DeletedByUser { get; set; }
    public DateTime? DeletedAt { get; set; }
}