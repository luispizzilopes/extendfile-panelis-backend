using ErrorOr;
using ExtendFile.Panelis.Application.Modules.Cat.Requests.UpdateCat;
using ExtendFile.Panelis.Application.Modules.Cat.Responses;
using ExtendFile.Panelis.Domain.Interfaces.UnitOfWork;
using ExtendFile.Panelis.Domain.Modules.Cat.ValueObject;
using ExtendFile.Panelis.Domain.Modules.House.ValueObject;

namespace ExtendFile.Panelis.Application.Modules.Cat.UseCases.UpdateCat;

public class UpdateCatUseCase
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateCatUseCase(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<CatDto>> ExecuteAsync(UpdateCatRequest request, CancellationToken cancellationToken = default)
    {
        var catId = CatId.Create(request.Id);
        var cat = await _unitOfWork.CatRepository.GetCatByIdAsync(catId.Value, cancellationToken);
        
        if (cat is null)
        {
            return Error.NotFound("Gato não encontrado", "Gato não encontrado");
        }

        var boxId = BoxId.Create(request.IsActive ? request.BoxId : Guid.Empty);
        
        var box = await _unitOfWork.HouseRepository.GetBoxByIdAsync(request.BoxId, cancellationToken);
        if (box is null)
            return Error.NotFound("Box não encontrado", "Box não encontrado");
        
        var currentCatsCount = await _unitOfWork.CatRepository.GetCatsCountByBoxAsync(request.BoxId, cancellationToken);
        
        if (cat.BoxId.Value != request.BoxId)
            currentCatsCount++;
        
        if (currentCatsCount > box.MaxQuantity)
            return Error.Validation("Box está cheio", $"O box '{box.Name}' já está totalmente ocupado. Capacidade máxima: {box.MaxQuantity}");
        
        cat.Update(
            request.Name,
            request.Hash,
            request.Age,
            request.Weight,
            request.Sex,
            request.IsActive
        );
        
        cat.MoveToBox(boxId);
        
        await _unitOfWork.CatRepository.UpdateCatAsync(cat, cancellationToken);
        await _unitOfWork.CommitAsync(cancellationToken);

        return new CatDto
        {
            Id = cat.Id.Value,
            Name = cat.Name,
            Hash = cat.Hash,
            Age = cat.Age,
            Weight = cat.Weight,
            Sex = cat.Sex,
            BoxId = cat.BoxId.Value,
            CreatedAt = cat.CreatedAt,
            UpdatedAt = cat.UpdatedAt,
            IsActive = cat.IsActive
        };
    }
}
