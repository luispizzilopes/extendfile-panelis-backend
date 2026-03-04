using ErrorOr;
using ExtendFile.Panelis.Application.Modules.Dashboard.Responses.GetDashboard;
using ExtendFile.Panelis.Application.Modules.Dashboard.UseCases.GetDashboard;
using MediatR;

namespace ExtendFile.Panelis.Application.Modules.Dashboard.Queries.GetDashboard;

public class GetDashboardQueryHandler : IRequestHandler<GetDashboardQuery, ErrorOr<DashboardResponse>>
{
    private readonly GetDashboardUseCase _getDashboardUseCase;

    public GetDashboardQueryHandler(GetDashboardUseCase getDashboardUseCase)
    {
        _getDashboardUseCase = getDashboardUseCase;
    }

    public async Task<ErrorOr<DashboardResponse>> Handle(GetDashboardQuery request, CancellationToken cancellationToken)
    {
        return await _getDashboardUseCase.ExecuteAsync(request.Request, cancellationToken);
    }
}
