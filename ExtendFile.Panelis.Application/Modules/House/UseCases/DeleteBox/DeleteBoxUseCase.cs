using ErrorOr;
using ExtendFile.Panelis.Application.Modules.House.Requests.DeleteBox;
using ExtendFile.Panelis.Application.Modules.House.Responses.DeleteBox;
using ExtendFile.Panelis.Domain.Interfaces.UnitOfWork;

namespace ExtendFile.Panelis.Application.Modules.House.UseCases.DeleteBox;

public class DeleteBoxUseCase
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteBoxUseCase(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<DeleteBoxResponse>> ExecuteAsync(DeleteBoxRequest request, CancellationToken cancellationToken = default)
    {
        var house = await _unitOfWork.HouseRepository.GetHouseByIdAsync(request.HouseId, cancellationToken);
        
        if (house is null)
            return Error.NotFound(description: "Casa/Prédio não encontrada");

        var box = house.Boxes.FirstOrDefault(b => b.Id.Value == request.Id);
        
        if (box is null)
            return Error.NotFound(description: "Box não encontrado");

        house.RemoveBox(box);
        
        await _unitOfWork.HouseRepository.UpdateHouseAsync(house, cancellationToken);
        await _unitOfWork.CommitAsync(cancellationToken);

        return new DeleteBoxResponse
        {
            Success = true,
            Message = "Box excluído com sucesso"
        };
    }
}
