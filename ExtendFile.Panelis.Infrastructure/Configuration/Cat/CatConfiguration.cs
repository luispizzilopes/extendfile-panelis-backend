using ExtendFile.Panelis.Domain.Modules.Cat.ValueObject;
using ExtendFile.Panelis.Domain.Modules.House.ValueObject;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExtendFile.Panelis.Infrastructure.Configuration.Cat;

public class CatConfiguration : IEntityTypeConfiguration<Domain.Modules.Cat.Aggregates.Cat>
{
    public void Configure(EntityTypeBuilder<Domain.Modules.Cat.Aggregates.Cat> builder)
    {
        builder.ToTable("Cats");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Id)
            .HasConversion(id => id.Value, value => CatId.Create(value))
            .IsRequired();

        builder.Property(c => c.Name)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(c => c.Hash)
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(c => c.DateOfBirth).IsRequired();

        builder.Property(c => c.Weight)
            .HasPrecision(5, 2)
            .IsRequired();

        builder.Property(c => c.Sex)
            .HasConversion<int>()
            .IsRequired();

        builder.Property(c => c.Location)
            .HasConversion<int>();

        builder.Property(c => c.BoxId)
            .HasConversion(id => id.Value, value => BoxId.Create(value))
            .IsRequired();

        builder.Property(c => c.CreatedAt).IsRequired();
        builder.Property(c => c.UpdatedAt);
        
        builder.Property(c => c.IsActive)
            .IsRequired();

        builder.Property(c => c.DaysWithoutEating);
    }
}