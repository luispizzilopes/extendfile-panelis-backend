using ExtendFile.Panelis.Application.Modules.Dashboard.Models.ReadModel;

namespace ExtendFile.Panelis.Application.Modules.Dashboard.Interfaces.Repositories;

public interface IDashboardReadModelRepository
{
    Task<DashboardReadModel?> GetAsync(CancellationToken cancellationToken);
}