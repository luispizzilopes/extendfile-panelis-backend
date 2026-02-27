using ExtendFile.Panelis.Domain.Modules.House.ValueObject;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExtendFile.Panelis.Infrastructure.Configuration.House;

public class HouseConfiguration : IEntityTypeConfiguration<Domain.Modules.House.Aggregates.House>
{
    public void Configure(EntityTypeBuilder<Domain.Modules.House.Aggregates.House> builder)
    {
        builder.ToTable("Houses");

        builder.HasKey(h => h.Id);

        builder.Property(h => h.Id)
            .HasConversion(id => id.Value, value => HouseId.Create(value))
            .IsRequired();

        builder.Property(h => h.Name)
            .HasMaxLength(100)
            .IsRequired();
        
        builder.Property(h => h.Description)
            .HasMaxLength(500)
            .IsRequired();

        builder.Property(h => h.CreatedAt).IsRequired();
        builder.Property(h => h.UpdatedAt);

        builder.HasMany(h => h.Boxes)
            .WithOne()
            .HasForeignKey("HouseId")
            .OnDelete(DeleteBehavior.Cascade);
    }
}