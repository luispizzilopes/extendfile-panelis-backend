using ErrorOr;
using ExtendFile.Panelis.Application.Modules.House.Responses;
using ExtendFile.Panelis.Application.Modules.House.UseCases.GetHouses;
using ExtendFile.Panelis.BuildingBlocks.Pagination;
using MediatR;

namespace ExtendFile.Panelis.Application.Modules.House.Queries.GetHouses;

public class GetHousesQueryHandler : IRequestHandler<GetHousesQuery, ErrorOr<PaginedResult<HouseDto>>>
{
    private readonly GetHousesUseCase _getHousesUseCase;

    public GetHousesQueryHandler(GetHousesUseCase getHousesUseCase)
    {
        _getHousesUseCase = getHousesUseCase;
    }

    public async Task<ErrorOr<PaginedResult<HouseDto>>> Handle(GetHousesQuery request, CancellationToken cancellationToken)
    {
        return await _getHousesUseCase.ExecuteAsync(request.Request, cancellationToken);
    }
}
