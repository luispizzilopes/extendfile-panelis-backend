using ErrorOr;
using ExtendFile.Panelis.Application.Modules.House.Requests.UpdateBox;
using ExtendFile.Panelis.Application.Modules.House.Responses;
using ExtendFile.Panelis.Domain.Interfaces.UnitOfWork;
using ExtendFile.Panelis.Domain.Modules.House.Entities;

namespace ExtendFile.Panelis.Application.Modules.House.UseCases.UpdateBox;

public class UpdateBoxUseCase
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateBoxUseCase(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<BoxDto>> ExecuteAsync(UpdateBoxRequest request, CancellationToken cancellationToken = default)
    {
        var house = await _unitOfWork.HouseRepository.GetHouseByIdAsync(request.HouseId, cancellationToken);
        
        if (house is null)
            return Error.NotFound(description: "Casa/Prédio não encontrada");

        var box = house.Boxes.FirstOrDefault(b => b.Id.Value == request.Id);
        
        if (box is null)
            return Error.NotFound(description: "Box não encontrado");

        box.Update(request.Name, request.MaxQuantity);
        
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
