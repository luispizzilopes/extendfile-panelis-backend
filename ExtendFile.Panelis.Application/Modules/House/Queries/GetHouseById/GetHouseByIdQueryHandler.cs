using ErrorOr;
using ExtendFile.Panelis.Application.Modules.House.Responses;
using ExtendFile.Panelis.Application.Modules.House.UseCases.GetHouseById;
using MediatR;

namespace ExtendFile.Panelis.Application.Modules.House.Queries.GetHouseById;

public class GetHouseByIdQueryHandler : IRequestHandler<GetHouseByIdQuery, ErrorOr<HouseDto>>
{
    private readonly GetHouseByIdUseCase _getHouseByIdUseCase;

    public GetHouseByIdQueryHandler(GetHouseByIdUseCase getHouseByIdUseCase)
    {
        _getHouseByIdUseCase = getHouseByIdUseCase;
    }

    public async Task<ErrorOr<HouseDto>> Handle(GetHouseByIdQuery request, CancellationToken cancellationToken)
    {
        return await _getHouseByIdUseCase.ExecuteAsync(request.Request.Id, cancellationToken);
    }
}