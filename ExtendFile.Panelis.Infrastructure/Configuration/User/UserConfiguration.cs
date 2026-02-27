using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExtendFile.Panelis.Infrastructure.Configuration.User;

public class UserConfiguration : IEntityTypeConfiguration<Domain.Modules.User.Entities.User>
{
    public void Configure(EntityTypeBuilder<Domain.Modules.User.Entities.User> builder)
    {
        builder.ToTable("AspNetUsers");
    }
}