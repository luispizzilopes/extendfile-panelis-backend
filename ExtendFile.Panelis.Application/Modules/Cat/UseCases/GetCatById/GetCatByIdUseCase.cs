using ErrorOr;
using ExtendFile.Panelis.Application.Modules.Cat.Requests.GetCatById;
using ExtendFile.Panelis.Application.Modules.Cat.Responses;
using ExtendFile.Panelis.Domain.Interfaces.UnitOfWork;

namespace ExtendFile.Panelis.Application.Modules.Cat.UseCases.GetCatById;

public class GetCatByIdUseCase
{
    private readonly IUnitOfWork _unitOfWork;

    public GetCatByIdUseCase(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<CatDto>> ExecuteAsync(GetCatByIdRequest request, CancellationToken cancellationToken = default)
    {
        var cat = await _unitOfWork.CatRepository.GetCatByIdAsync(request.Id, cancellationToken);
        
        if (cat is null)
        {
            return Error.NotFound("Cat not found", "Gato não encontrado");
        }
         
        var house = await _unitOfWork.HouseRepository
            .GetHouseByBoxIdAsync(cat.BoxId.Value, cancellationToken);

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
            IsActive = cat.IsActive,
            HouseName = house?.Name ?? string.Empty,
            BoxName = house?.Boxes?
                .Where(x => x.Id.Value == cat.BoxId.Value)
                .FirstOrDefault()?.Name ?? string.Empty
        };
    }
}
