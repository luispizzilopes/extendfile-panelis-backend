using ExtendFile.Panelis.BuildingBlocks.Pagination;
using ExtendFile.Panelis.Domain.Modules.Cat.Enums;

namespace ExtendFile.Panelis.Domain.Interfaces.Repositories.Cat;

public interface ICatRepository
{
    Task<Modules.Cat.Aggregates.Cat?> GetCatByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<Modules.Cat.Aggregates.Cat?> GetCatByHashAsync(string hash, CancellationToken cancellationToken);
    Task<PaginedResult<Modules.Cat.Aggregates.Cat>> GetCatsByBoxIdAsync(PaginationParams paginationParams, Guid boxId, CancellationToken cancellationToken);
    Task<IEnumerable<Modules.Cat.Aggregates.Cat>> GetCatsByBoxIdAsync(Guid boxId, CancellationToken cancellationToken);
    Task<PaginedResult<Modules.Cat.Aggregates.Cat>> GetCatsAsync(
        PaginationParams paginationParams, 
        string? name,
        CatLocation? location,
        bool? isActive,
        CatSex? sex,
        CancellationToken cancellationToken);
    Task<IEnumerable<Modules.Cat.Aggregates.Cat>> GetAllCatsAsync(CancellationToken cancellationToken);
    Task<IEnumerable<Modules.Cat.Aggregates.Cat>> GetCatsWithoutEatingAsync(int alertDays, int warningDays, CancellationToken cancellationToken);
    Task<int> GetCatsCountByBoxAsync(Guid boxId, CancellationToken cancellationToken);
    Task<Dictionary<Guid, int>> GetCatsCountByBoxesAsync(IEnumerable<Guid>? boxIds, CancellationToken cancellationToken);
    
    Task CreateCatAsync(Modules.Cat.Aggregates.Cat cat, CancellationToken cancellationToken);
    Task UpdateCatAsync(Modules.Cat.Aggregates.Cat cat, CancellationToken cancellationToken);
    Task DeleteCatAsync(Guid catId, CancellationToken cancellationToken);
}