using ErrorOr;
using ExtendFile.Panelis.Application.Modules.Dashboard.Interfaces.Repositories;
using ExtendFile.Panelis.Application.Modules.Dashboard.Requests.GetDashboard;
using ExtendFile.Panelis.Application.Modules.Dashboard.Responses.GetDashboard;
using ExtendFile.Panelis.Domain.Interfaces.UnitOfWork;

namespace ExtendFile.Panelis.Application.Modules.Dashboard.UseCases.GetDashboard;

public class GetDashboardUseCase
{
    private readonly IDashboardReadModelRepository _dashboardReadModelRepository;

    public GetDashboardUseCase(IDashboardReadModelRepository dashboardReadModelRepository)
    {
        _dashboardReadModelRepository = dashboardReadModelRepository;
    }

    public async Task<ErrorOr<DashboardResponse>> ExecuteAsync(
        GetDashboardRequest request, 
        CancellationToken cancellationToken = default)
    {
        var response = await _dashboardReadModelRepository.GetAsync(cancellationToken);

        return new DashboardResponse
        {
            TotalCats = response?.TotalCats ?? 0,
            TotalHouses = response?.TotalHouses ?? 0,
            TotalBoxes = response?.TotalBoxes ?? 0,
            TotalFullBoxes = response?.TotalFullBoxes ?? 0,
        };
    }
}
