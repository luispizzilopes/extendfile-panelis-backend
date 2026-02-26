using ErrorOr;
using ExtendFile.Panelis.Application.Modules.House.Responses;
using ExtendFile.Panelis.Application.Modules.House.UseCases.GetAllHouses;
using MediatR;

namespace ExtendFile.Panelis.Application.Modules.House.Queries.GetAllHouses;

public class GetAllHousesQueryHandler : IRequestHandler<GetAllHousesQuery, ErrorOr<IEnumerable<HouseDto>>>
{
    private readonly GetAllHousesUseCase _getAllHousesUseCase;

    public GetAllHousesQueryHandler(GetAllHousesUseCase getAllHousesUseCase)
    {
        _getAllHousesUseCase = getAllHousesUseCase;
    }

    public async Task<ErrorOr<IEnumerable<HouseDto>>> Handle(GetAllHousesQuery request, CancellationToken cancellationToken)
    {
        return await _getAllHousesUseCase.ExecuteAsync(cancellationToken);
    }
}
