using LeafyAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(u => u.isOnboarded)
            .HasDefaultValue(false);

        builder.HasOne(u => u.Levels)
            .WithOne(l => l.User)
            .HasForeignKey<Levels>(l => l.UserId);
    }
}