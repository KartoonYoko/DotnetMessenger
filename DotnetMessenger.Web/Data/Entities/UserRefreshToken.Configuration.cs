using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DotnetMessenger.Web.Data.Entities;

public class UserRefreshTokenConfiguration
    : IEntityTypeConfiguration<UserRefreshToken>
{
    public void Configure(EntityTypeBuilder<UserRefreshToken> builder)
    {
        builder
            .HasIndex(x => x.Token)
            .IsUnique();
    }
}