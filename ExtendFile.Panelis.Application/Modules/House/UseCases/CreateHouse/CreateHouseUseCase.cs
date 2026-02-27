using ErrorOr;
using ExtendFile.Panelis.Application.Modules.House.Requests.CreateHouse;
using ExtendFile.Panelis.Application.Modules.House.Responses;
using ExtendFile.Panelis.Domain.Interfaces.UnitOfWork;

namespace ExtendFile.Panelis.Application.Modules.House.UseCases.CreateHouse;

public class CreateHouseUseCase
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateHouseUseCase(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<HouseDto>> ExecuteAsync(CreateHouseRequest request, CancellationToken cancellationToken = default)
    {
        var house = Domain.Modules.House.Aggregates.House.Create(request.Name, request.Description);
        
        await _unitOfWork.HouseRepository.CreateHouseAsync(house, cancellationToken);
        await _unitOfWork.CommitAsync(cancellationToken);

        return new HouseDto
        {
            Id = house.Id.Value,
            Name = house.Name,
            Description = house.Description,
            CreatedAt = house.CreatedAt,
            UpdatedAt = house.UpdatedAt,
        };
    }
}
