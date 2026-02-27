using ExtendFile.Panelis.Domain.Modules.House.ValueObject;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExtendFile.Panelis.Infrastructure.Configuration.Box;


public class BoxConfiguration : IEntityTypeConfiguration<Domain.Modules.House.Entities.Box>
{
    public void Configure(EntityTypeBuilder<Domain.Modules.House.Entities.Box> builder)
    {
        builder.ToTable("Boxes");

        builder.HasKey(b => b.Id);

        builder.Property(b => b.Id)
            .HasConversion(id => id.Value, value => BoxId.Create(value))
            .IsRequired();

        builder.Property(b => b.MaxQuantity)
            .IsRequired();

        builder.Property(b => b.Name)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(b => b.CreatedAt).IsRequired();
        builder.Property(b => b.UpdatedAt);

        builder.Property<HouseId>("HouseId")
            .HasConversion(id => id.Value, value => HouseId.Create(value))
            .IsRequired();
    }
}