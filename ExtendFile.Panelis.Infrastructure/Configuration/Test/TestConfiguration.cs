using ExtendFile.Panelis.Domain.Modules.House.ValueObject;
using ExtendFile.Panelis.Domain.Modules.Test.ValueObject;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExtendFile.Panelis.Infrastructure.Configuration.Test;

public class TestConfiguration : IEntityTypeConfiguration<Domain.Modules.Test.Aggregates.Test>
{
    public void Configure(EntityTypeBuilder<Domain.Modules.Test.Aggregates.Test> builder)
    {
        builder.ToTable("Tests");

        builder.HasKey(t => t.Id);

        builder.Property(t => t.Id)
            .HasConversion(id => id.Value, value => TestId.Create(value))
            .IsRequired();

        builder.Property(t => t.Name)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(t => t.FileName)
            .HasMaxLength(500)
            .IsRequired();

        builder.Property(t => t.BoxId)
            .HasConversion(id => id.Value, value => BoxId.Create(value))
            .IsRequired();

        builder.Property(t => t.TestDate)
            .IsRequired();

        builder.Property(t => t.CreatedAt)
            .IsRequired();

        builder.HasMany(t => t.Lines)
            .WithOne()
            .HasForeignKey("TestId")
            .OnDelete(DeleteBehavior.Cascade);
    }
}
