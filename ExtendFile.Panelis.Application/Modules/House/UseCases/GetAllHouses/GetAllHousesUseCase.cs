using ErrorOr;
using ExtendFile.Panelis.Application.Modules.House.Responses;
using ExtendFile.Panelis.Domain.Interfaces.UnitOfWork;
using ExtendFile.Panelis.Domain.Modules.House.Aggregates;

namespace ExtendFile.Panelis.Application.Modules.House.UseCases.GetAllHouses;

public class GetAllHousesUseCase
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAllHousesUseCase(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<IEnumerable<HouseDto>>> ExecuteAsync(CancellationToken cancellationToken = default)
    {
        var houses = await _unitOfWork.HouseRepository.GetAllHousesAsync(cancellationToken);

        var result = houses.Select(house => new HouseDto
        {
            Id = house.Id.Value,
            Name = house.Name,
            CreatedAt = house.CreatedAt,
            UpdatedAt = house.UpdatedAt
        }).ToList();
        
        return result;
    }
}
