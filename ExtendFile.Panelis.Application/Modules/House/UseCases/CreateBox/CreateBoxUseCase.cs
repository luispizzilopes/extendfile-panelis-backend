using ErrorOr;
using ExtendFile.Panelis.Application.Modules.House.Requests.CreateBox;
using ExtendFile.Panelis.Application.Modules.House.Requests.CreateHouse;
using ExtendFile.Panelis.Application.Modules.House.Responses;
using ExtendFile.Panelis.Domain.Interfaces.UnitOfWork;
using ExtendFile.Panelis.Domain.Modules.House.Entities;

namespace ExtendFile.Panelis.Application.Modules.House.UseCases.CreateBox;

public class CreateBoxUseCase
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateBoxUseCase(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<BoxDto>> ExecuteAsync(CreateBoxRequest request, CancellationToken cancellationToken = default)
    {
        var house = await _unitOfWork.HouseRepository
            .GetHouseByIdAsync(request.HouseId, cancellationToken);
        
        if (house is null)
            return Error.NotFound(description: "Casa/Prédio não encontrada");

        var box = Box.Create(request.Name); 
        house.AddBox(box);
        
        await _unitOfWork.HouseRepository.UpdateHouseAsync(house, cancellationToken);
        await _unitOfWork.CommitAsync(cancellationToken);

        return new BoxDto
        {
            Id = box.Id.Value,
            Name = box.Name,
            CreatedAt = box.CreatedAt,
            UpdatedAt = box.UpdatedAt,
            HouseId = house.Id.Value,
        };
    }
}