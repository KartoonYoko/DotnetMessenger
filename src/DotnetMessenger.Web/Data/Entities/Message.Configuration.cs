using DotnetMessenger.Web.Data.Abstractions;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DotnetMessenger.Web.Data.Entities;

public class MessageConfiguration
    : AuditableEntityBaseConfiguration<Message>
{
    public new void Configure(EntityTypeBuilder<Message> builder)
    {
        base.Configure(builder);
    }
}