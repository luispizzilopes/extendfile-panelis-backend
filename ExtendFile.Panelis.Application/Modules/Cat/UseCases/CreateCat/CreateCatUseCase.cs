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
