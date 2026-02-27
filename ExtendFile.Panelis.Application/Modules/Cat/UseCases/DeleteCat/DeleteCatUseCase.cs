using ErrorOr;
using ExtendFile.Panelis.Application.Modules.Cat.Requests.DeleteCat;
using ExtendFile.Panelis.Application.Modules.Cat.Responses.DeleteCat;
using ExtendFile.Panelis.Domain.Interfaces.UnitOfWork;
using ExtendFile.Panelis.Domain.Modules.Cat.ValueObject;

namespace ExtendFile.Panelis.Application.Modules.Cat.UseCases.DeleteCat;

public class DeleteCatUseCase
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteCatUseCase(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<DeleteCatResponse>> ExecuteAsync(DeleteCatRequest request, CancellationToken cancellationToken = default)
    {
        var catId = CatId.Create(request.Id);
        var cat = await _unitOfWork.CatRepository.GetCatByIdAsync(catId.Value, cancellationToken);
        
        if (cat is null)
        {
            return Error.NotFound("Cat not found", "Gato não encontrado");
        }

        await _unitOfWork.CatRepository.DeleteCatAsync(catId.Value, cancellationToken);
        await _unitOfWork.CommitAsync(cancellationToken);

        return new DeleteCatResponse
        {
            Success = true,
            Message = "Gato deletado com sucesso"
        };
    }
}
