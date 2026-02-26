using ErrorOr;
using ExtendFile.Panelis.Application.Modules.House.Requests.UpdateHouse;
using ExtendFile.Panelis.Application.Modules.House.Responses;
using ExtendFile.Panelis.Domain.Interfaces.UnitOfWork;
using ExtendFile.Panelis.Domain.Modules.House.Aggregates;

namespace ExtendFile.Panelis.Application.Modules.House.UseCases.UpdateHouse;

public class UpdateHouseUseCase
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateHouseUseCase(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<HouseDto>> ExecuteAsync(UpdateHouseRequest request, CancellationToken cancellationToken = default)
    {
        var house = await _unitOfWork.HouseRepository.GetHouseByIdAsync(request.Id, cancellationToken);
        
        if (house is null)
            return Error.NotFound(description: "Casa/Prédio não encontrada");

        house.Update(request.Name);
        
        await _unitOfWork.HouseRepository.UpdateHouseAsync(house, cancellationToken);
        await _unitOfWork.CommitAsync(cancellationToken);

        return new HouseDto
        {
            Id = house.Id.Value,
            Name = house.Name,
            CreatedAt = house.CreatedAt,
            UpdatedAt = house.UpdatedAt!.Value
        };
    }
}
