using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DotnetMessenger.Web.Data.Abstractions;

public abstract class AuditableEntityBaseConfiguration<T>
    : IEntityTypeConfiguration<T> where T : AuditableEntityBase
{
    public void Configure(EntityTypeBuilder<T> builder)
    {
        builder
            .HasOne(x => x.CreatedByUser)
            .WithMany()
            .HasForeignKey(x => x.CreatedBy);
        
        builder
            .HasOne(x => x.UpdatedByUser)
            .WithMany()
            .HasForeignKey(x => x.UpdateBy);
        
        builder
            .HasOne(x => x.DeletedByUser)
            .WithMany()
            .HasForeignKey(x => x.DeletedBy);

        builder
            .HasIndex(x => x.DeletedBy)
            .HasFilter("deleted_by is not null");
    }
}