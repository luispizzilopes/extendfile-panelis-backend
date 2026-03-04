using ExtendFile.Panelis.Application.Modules.Dashboard.Interfaces.Repositories;
using ExtendFile.Panelis.Application.Modules.Dashboard.Models.ReadModel;
using ExtendFile.Panelis.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace ExtendFile.Panelis.Infrastructure.Repositories.Dashboard;

public class DashboardReadModelRepository  : IDashboardReadModelRepository
{
    private readonly AppDbContext  _context;

    public DashboardReadModelRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<DashboardReadModel?> GetAsync(CancellationToken cancellationToken)
    {
        return await _context .Dashboards
            .FirstOrDefaultAsync(cancellationToken);
    }
}