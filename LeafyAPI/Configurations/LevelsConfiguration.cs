using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using LeafyAPI.Models;

public class LevelsConfiguration : IEntityTypeConfiguration<Level>
{
    public void Configure(EntityTypeBuilder<Level> builder)
    {
        builder.Property(l => l.CurrentLevel)
            .HasDefaultValue(1);

        builder.Property(l => l.ExperiencePoints)
            .HasDefaultValue(0);

        builder.Property(l => l.ExperienceThreshold)
            .HasDefaultValue(100);
    }
}