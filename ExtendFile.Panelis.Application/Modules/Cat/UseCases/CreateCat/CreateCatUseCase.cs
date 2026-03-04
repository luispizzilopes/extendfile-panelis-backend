using ErrorOr;
using ExtendFile.Panelis.Application.Modules.Cat.Requests.CreateCat;
using ExtendFile.Panelis.Application.Modules.Cat.Responses;
using ExtendFile.Panelis.Domain.Interfaces.UnitOfWork;
using ExtendFile.Panelis.Domain.Modules.House.ValueObject;

namespace ExtendFile.Panelis.Application.Modules.Cat.UseCases.CreateCat;

public class CreateCatUseCase
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateCatUseCase(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<CatDto>> ExecuteAsync(CreateCatRequest request, CancellationToken cancellationToken = default)
    {
        var boxId = BoxId.Create(request.BoxId);
        
        var box = await _unitOfWork.HouseRepository.GetBoxByIdAsync(request.BoxId, cancellationToken);
        if (box is null)
            return Error.NotFound("Box não encontrado", "Box não encontrado");
        
        var currentCatsCount = await _unitOfWork.CatRepository.GetCatsCountByBoxAsync(request.BoxId, cancellationToken);
        if (currentCatsCount >= box.MaxQuantity)
            return Error.Validation("Box está cheio", $"O box '{box.Name}' já está totalmente ocupado. Capacidade máxima: {box.MaxQuantity}");
        
        var cat = Domain.Modules.Cat.Aggregates.Cat.Create(
            request.Name,
            request.Hash,
            request.Age,
            request.Weight,
            request.Sex,
            boxId
        );
        
        await _unitOfWork.CatRepository.CreateCatAsync(cat, cancellationToken);
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
