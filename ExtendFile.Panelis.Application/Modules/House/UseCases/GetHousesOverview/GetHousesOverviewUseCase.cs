using ErrorOr;
using ExtendFile.Panelis.Application.Modules.Cat.Responses;
using ExtendFile.Panelis.Application.Modules.House.Requests.GetHousesOverview;
using ExtendFile.Panelis.Application.Modules.House.Responses;
using ExtendFile.Panelis.Domain.Interfaces.UnitOfWork;

namespace ExtendFile.Panelis.Application.Modules.House.UseCases.GetHousesOverview;

public class GetHousesOverviewUseCase
{
    private readonly IUnitOfWork _unitOfWork;

    public GetHousesOverviewUseCase(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<IEnumerable<HouseOverviewDto>>> ExecuteAsync(GetHousesOverviewRequest request, CancellationToken cancellationToken = default)
    {
        var houses = await _unitOfWork.HouseRepository.GetAllHousesWithBoxesAsync(cancellationToken);

        var houseList = houses?.ToList() ?? [];

        var boxIds = houseList
            .SelectMany(h => h.Boxes ?? [])
            .Select(b => b.Id.Value)
            .ToList();

        var daysMap = boxIds.Count > 0
            ? await _unitOfWork.TestRepository.GetCountDaysWithoutTestBatchAsync(boxIds, cancellationToken)
            : new Dictionary<Guid, int?>();

        var result = new List<HouseOverviewDto>();

        foreach (var house in houseList)
        {
            var boxes = new List<BoxOverviewDto>();

            foreach (var box in house.Boxes ?? [])
            {
                var cats = await _unitOfWork.CatRepository.GetCatsByBoxIdAsync(box.Id.Value, cancellationToken);

                var catDtos = cats?.Select(cat => new CatDto
                {
                    Id = cat.Id.Value,
                    Name = cat.Name,
                    Hash = cat.Hash,
                    DateOfBirth = cat.DateOfBirth,
                    Weight = cat.Weight,
                    Sex = cat.Sex,
                    Location = cat.Location,
                    BoxId = cat.BoxId.Value,
                    CreatedAt = cat.CreatedAt,
                    UpdatedAt = cat.UpdatedAt,
                    IsActive = cat.IsActive,
                    HouseName = house.Name,
                    BoxName = box.Name
                }).ToList() ?? [];

                boxes.Add(new BoxOverviewDto
                {
                    Id = box.Id.Value,
                    Name = box.Name,
                    HouseId = house.Id.Value,
                    MaxQuantity = box.MaxQuantity,
                    Quantity = catDtos.Count,
                    DaysWithoutTest = daysMap.GetValueOrDefault(box.Id.Value) - 1,
                    CreatedAt = box.CreatedAt,
                    UpdatedAt = box.UpdatedAt,
                    Cats = catDtos
                });
            }

            result.Add(new HouseOverviewDto
            {
                Id = house.Id.Value,
                Name = house.Name,
                Description = house.Description,
                CreatedAt = house.CreatedAt,
                UpdatedAt = house.UpdatedAt,
                Boxes = boxes
            });
        }

        return result;
    }
}
