using ExtendFile.Panelis.Domain.Modules.Cat.ValueObject;
using ExtendFile.Panelis.Domain.Modules.Test.ValueObject;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExtendFile.Panelis.Infrastructure.Configuration.TestLine;

public class TestLineConfiguration : IEntityTypeConfiguration<Domain.Modules.Test.Entities.TestLine>
{
    public void Configure(EntityTypeBuilder<Domain.Modules.Test.Entities.TestLine> builder)
    {
        builder.ToTable("TestLines");

        builder.HasKey(tl => tl.Id);

        builder.Property(tl => tl.Id)
            .HasConversion(id => id.Value, value => TestLineId.Create(value))
            .IsRequired();

        builder.Property(tl => tl.Position)
            .IsRequired();

        builder.Property(tl => tl.CatName)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(tl => tl.CatId)
            .HasConversion(id => id.Value, value => CatId.Create(value))
            .IsRequired();

        builder.Property(tl => tl.FirstFood)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(tl => tl.SecondFood)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(tl => tl.TotalAmountFood)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(tl => tl.CatHash)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(tl => tl.FoodAmountStatus)
            .IsRequired();
    }
}
