using ErrorOr;
using ExtendFile.Panelis.Application.Modules.House.Requests.DeleteHouse;
using ExtendFile.Panelis.Application.Modules.House.Responses.DeleteHouse;
using ExtendFile.Panelis.Domain.Interfaces.UnitOfWork;

namespace ExtendFile.Panelis.Application.Modules.House.UseCases.DeleteHouse;

public class DeleteHouseUseCase
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteHouseUseCase(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<DeleteHouseResponse>> ExecuteAsync(DeleteHouseRequest request, CancellationToken cancellationToken = default)
    {
        var house = await _unitOfWork.HouseRepository.GetHouseByIdAsync(request.Id, cancellationToken);
        
        if (house is null)
            return Error.NotFound(description: "Casa/Prédio não encontrada");

        await _unitOfWork.HouseRepository.DeleteHouseAsync(request.Id, cancellationToken);
        await _unitOfWork.CommitAsync(cancellationToken);

        return new DeleteHouseResponse
        {
            Success = true,
            Message = "Casa/Prédio excluída com sucesso"
        };
    }
}
