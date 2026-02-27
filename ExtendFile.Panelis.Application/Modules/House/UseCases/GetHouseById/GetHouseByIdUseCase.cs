using ErrorOr;
using ExtendFile.Panelis.Application.Modules.House.Responses;
using ExtendFile.Panelis.Domain.Interfaces.UnitOfWork;
using ExtendFile.Panelis.Domain.Modules.House.Aggregates;

namespace ExtendFile.Panelis.Application.Modules.House.UseCases.GetHouseById;

public class GetHouseByIdUseCase
{
    private readonly IUnitOfWork _unitOfWork;

    public GetHouseByIdUseCase(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<HouseDto>> ExecuteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var result = await _unitOfWork.HouseRepository
            .GetHouseByIdAsync(id, cancellationToken);
        
        if (result is null)
            return Error.NotFound(description: "Casa/Prédio não encontrada");
            
        return new HouseDto
        {
            Id = result.Id.Value,
            Name = result.Name,
            Description = result.Description,
            CreatedAt = result.CreatedAt,
            UpdatedAt = result.UpdatedAt,
            Boxes = result.Boxes.Select(x => new BoxDto
            {
                Id = x.Id.Value, 
                Name = x.Name, 
                CreatedAt = x.CreatedAt, 
                UpdatedAt = x.UpdatedAt, 
                HouseId = result.Id.Value 
            }).ToList()
        };
    }
}