using ErrorOr;
using ExtendFile.Panelis.Application.Modules.House.Responses;
using ExtendFile.Panelis.Application.Modules.House.UseCases.GetHousesOverview;
using MediatR;

namespace ExtendFile.Panelis.Application.Modules.House.Queries.GetHousesOverview;

public class GetHousesOverviewQueryHandler : IRequestHandler<GetHousesOverviewQuery, ErrorOr<IEnumerable<HouseOverviewDto>>>
{
    private readonly GetHousesOverviewUseCase _getHousesOverviewUseCase;

    public GetHousesOverviewQueryHandler(GetHousesOverviewUseCase getHousesOverviewUseCase)
    {
        _getHousesOverviewUseCase = getHousesOverviewUseCase;
    }

    public async Task<ErrorOr<IEnumerable<HouseOverviewDto>>> Handle(GetHousesOverviewQuery request, CancellationToken cancellationToken)
    {
        return await _getHousesOverviewUseCase.ExecuteAsync(request.Request, cancellationToken);
    }
}
