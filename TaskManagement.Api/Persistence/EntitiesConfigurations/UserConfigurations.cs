using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManagement.Api.Entities;

namespace TaskManagement.Api.Persistence.EntitiesConfigurations;

public class UserConfigurations : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder
            .OwnsMany(x => x.RefreshTokens)
            .ToTable("RefreshTokens")
            .WithOwner()
            .HasForeignKey("UserId");

        builder.Property(x => x.FullName).HasMaxLength(100);
    }
}
