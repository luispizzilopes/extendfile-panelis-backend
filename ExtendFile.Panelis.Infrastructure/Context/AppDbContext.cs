using ExtendFile.Panelis.Application.Modules.Dashboard.Models.ReadModel;
using ExtendFile.Panelis.BuildingBlocks.Common.Interfaces.Events;
using ExtendFile.Panelis.Domain.Modules.Cat.Aggregates;
using ExtendFile.Panelis.Domain.Modules.House.Aggregates;
using ExtendFile.Panelis.Domain.Modules.House.Entities;
using ExtendFile.Panelis.Domain.Modules.User.Entities;
using ExtendFile.Panelis.Infrastructure.Interceptor;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ExtendFile.Panelis.Infrastructure.Context;

public class AppDbContext : IdentityDbContext<User>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public virtual DbSet<House> Houses { get; set; }
    public virtual DbSet<Cat> Cats { get; set; }
    public virtual DbSet<Box> Boxes { get; set; }
    
    //Repositories ReadModel
    public virtual DbSet<DashboardReadModel> Dashboards { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        modelBuilder.Ignore<List<IDomainEvent>>();
    }
}