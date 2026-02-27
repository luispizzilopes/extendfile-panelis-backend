using ErrorOr;
using ExtendFile.Panelis.Application.Modules.Cat.Requests.GetCatsByBoxId;
using ExtendFile.Panelis.Application.Modules.Cat.Responses;
using ExtendFile.Panelis.BuildingBlocks.Pagination;
using ExtendFile.Panelis.Domain.Interfaces.UnitOfWork;

namespace ExtendFile.Panelis.Application.Modules.Cat.UseCases.GetCatsByBoxId;

public class GetCatsByBoxIdUseCase
{
    private readonly IUnitOfWork _unitOfWork;

    public GetCatsByBoxIdUseCase(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<PaginedResult<CatDto>>> ExecuteAsync(GetCatsByBoxIdRequest request, CancellationToken cancellationToken = default)
    {
        var catsResult = await _unitOfWork.CatRepository.GetCatsByBoxIdAsync(request.PaginationParams, request.BoxId, cancellationToken);
        
        var catDtos = catsResult.Data?.Select(cat => new CatDto
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
        }).ToList();
        
        foreach (var catDto in catDtos)
        {
            var house = await _unitOfWork.HouseRepository.GetHouseByBoxIdAsync(catDto.BoxId, cancellationToken);
            catDto.HouseName = house?.Name ?? string.Empty;;
            catDto.BoxName = house?.Boxes?.Where(x => x.Id.Value == catDto.BoxId).FirstOrDefault()?.Name ?? string.Empty;
        }

        return new PaginedResult<CatDto>
        {
            Data = catDtos,
            PageNumber = catsResult.PageNumber,
            PageSize = catsResult.PageSize,
            TotalRecords = catsResult.TotalRecords
        };
    }
}
