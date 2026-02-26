using ExtendFile.Panelis.BuildingBlocks.Pagination;

namespace ExtendFile.Panelis.Domain.Interfaces.Repositories.Cat;

public interface ICatRepository
{
    Task<Modules.Cat.Aggregates.Cat?> GetCatByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<PaginedResult<Modules.Cat.Aggregates.Cat>> GetCatsAsync(PaginationParams paginationParams, CancellationToken cancellationToken);
    Task<IEnumerable<Modules.Cat.Aggregates.Cat>> GetAllCatsAsync(CancellationToken cancellationToken);
    Task CreateCatAsync(Modules.Cat.Aggregates.Cat cat, CancellationToken cancellationToken);
    Task UpdateCatAsync(Modules.Cat.Aggregates.Cat cat, CancellationToken cancellationToken);
    Task DeleteCatAsync(Guid catId, CancellationToken cancellationToken);
}