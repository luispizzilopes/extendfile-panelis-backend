using ExtendFile.Panelis.Domain.Modules.Setting.Aggregates;
using ExtendFile.Panelis.Domain.Modules.Setting.ValueObject;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExtendFile.Panelis.Infrastructure.Configuration.Setting;

public class SettingConfiguration : IEntityTypeConfiguration<Domain.Modules.Setting.Aggregates.Setting>
{
    public void Configure(EntityTypeBuilder<Domain.Modules.Setting.Aggregates.Setting> builder)
    {
        builder.ToTable("Settings");

        builder.HasKey(s => s.Id);

        builder.Property(s => s.Id)
            .HasConversion(id => id.Value, value => SettingId.Create(value))
            .IsRequired();

        builder.Property(s => s.LessThanEnoughThreshold)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(s => s.MoreThanEnoughThreshold)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(s => s.CreatedAt)
            .IsRequired();

        builder.Property(s => s.UpdatedAt);
    }
}
