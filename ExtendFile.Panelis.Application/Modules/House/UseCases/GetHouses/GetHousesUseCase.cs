using ErrorOr;
using ExtendFile.Panelis.Application.Modules.House.Requests.GetHouses;
using ExtendFile.Panelis.Application.Modules.House.Responses;
using ExtendFile.Panelis.BuildingBlocks.Pagination;
using ExtendFile.Panelis.Domain.Interfaces.UnitOfWork;

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

        var data = MapToDto(result.Data);

        await EnrichWithDaysWithoutTestAsync(data, cancellationToken);
        await EnrichWithCatQuantitiesAsync(data, cancellationToken);

        return new PaginedResult<HouseDto>
        {
            Data = data,
            PageNumber = result.PageNumber,
            PageSize = result.PageSize,
            TotalRecords = result.TotalRecords
        };
    }

    private static List<HouseDto>? MapToDto(IEnumerable<Domain.Modules.House.Aggregates.House>? houses) =>
        houses?.Select(house => new HouseDto
        {
            Id = house.Id.Value,
            Name = house.Name,
            Description = house.Description,
            CreatedAt = house.CreatedAt,
            UpdatedAt = house.UpdatedAt,
            Boxes = house.Boxes?.Select(box => new BoxDto
            {
                Id = box.Id.Value,
                Name = box.Name,
                CreatedAt = box.CreatedAt,
                UpdatedAt = box.UpdatedAt,
                HouseId = house.Id.Value,
                MaxQuantity = box.MaxQuantity,
            }).ToList()
        }).ToList();

    private async Task EnrichWithDaysWithoutTestAsync(List<HouseDto>? data, CancellationToken cancellationToken)
    {
        if (data is not { Count: > 0 }) return;

        var boxIds = data
            .SelectMany(h => h.Boxes ?? [])
            .Select(b => b.Id)
            .ToList();

        if (boxIds.Count == 0) return;

        var daysMap = await _unitOfWork
            .TestRepository
            .GetCountDaysWithoutTestBatchAsync(boxIds, cancellationToken);

        foreach (var box in data.SelectMany(h => h.Boxes ?? []))
        {
            box.DaysWithoutTest = daysMap.GetValueOrDefault(box.Id, 0);
        }
    }

    private async Task EnrichWithCatQuantitiesAsync(List<HouseDto>? data, CancellationToken cancellationToken)
    {
        if (data is not { Count: > 0 }) return;

        var boxes = data
            .SelectMany(h => h.Boxes ?? [])
            .ToList();

        if (boxes.Count == 0) return;

        foreach (var box in boxes)
        {
            box.Quantity = await _unitOfWork
                .CatRepository
                .GetCatsCountByBoxAsync(box.Id, cancellationToken);
        }
    }
}