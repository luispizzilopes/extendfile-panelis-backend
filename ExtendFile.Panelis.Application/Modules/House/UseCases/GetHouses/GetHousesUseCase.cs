using ErrorOr;
using ExtendFile.Panelis.Application.Modules.House.Requests.GetHouses;
using ExtendFile.Panelis.Application.Modules.House.Responses;
using ExtendFile.Panelis.BuildingBlocks.Pagination;
using ExtendFile.Panelis.Domain.Interfaces.UnitOfWork;
using ExtendFile.Panelis.Domain.Modules.House.Aggregates;

namespace ExtendFile.Panelis.Application.Modules.House.UseCases.GetHouses;

public class GetHousesUseCase
{
    private readonly IUnitOfWork _unitOfWork;

    public GetHousesUseCase(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<PaginedResult<HouseDto>>> ExecuteAsync(GetHousesRequest request, CancellationToken cancellationToken = default)
    {
        var result = await _unitOfWork.HouseRepository.GetHousesAsync(request.PaginationParams, cancellationToken);

        var data = result.Data?.Select(house => new HouseDto
        {
            Id = house.Id.Value,
            Name = house.Name,
            CreatedAt = house.CreatedAt,
            UpdatedAt = house.UpdatedAt
        }).ToList();

        return new PaginedResult<HouseDto>
        {
            Data = data,
            PageNumber = result.PageNumber,
            PageSize = result.PageSize,
            TotalRecords = result.TotalRecords
        };
    }
}
