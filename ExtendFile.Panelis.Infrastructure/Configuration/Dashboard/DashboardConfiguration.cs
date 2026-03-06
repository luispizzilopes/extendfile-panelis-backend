using ExtendFile.Panelis.Application.Modules.Dashboard.Models.ReadModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExtendFile.Panelis.Infrastructure.Configuration.Dashboard;

public class DashboardConfiguration : IEntityTypeConfiguration<DashboardReadModel>
{
    public void Configure(EntityTypeBuilder<DashboardReadModel> builder)
    {
        builder.ToTable("vw_dashboard"); 
        builder.ToView("vw_dashboard");
        builder.HasNoKey();
        builder.Metadata.SetIsTableExcludedFromMigrations(true);

        builder.Property(x => x.TotalCats)
            .HasColumnName("totalcats");

        builder.Property(x => x.TotalHouses)
            .HasColumnName("totalhouses");

        builder.Property(x => x.TotalBoxes)
            .HasColumnName("totalboxes");

        builder.Property(x => x.TotalFullBoxes)
            .HasColumnName("totalfullboxes");
    }
}