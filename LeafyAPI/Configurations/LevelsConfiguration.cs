using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using LeafyAPI.Models;

public class LevelsConfiguration : IEntityTypeConfiguration<Levels>
{
    public void Configure(EntityTypeBuilder<Levels> builder)
    {
        builder.Property(l => l.Level)
            .HasDefaultValue(1);

        builder.Property(l => l.ExperiencePoints)
            .HasDefaultValue(0);

        builder.Property(l => l.ExperienceThreshold)
            .HasDefaultValue(100);
    }
}