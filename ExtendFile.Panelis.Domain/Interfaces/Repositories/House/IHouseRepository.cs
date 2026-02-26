using ExtendFile.Panelis.BuildingBlocks.Pagination;
using ExtendFile.Panelis.Domain.Modules.House.Aggregates;

namespace ExtendFile.Panelis.Domain.Interfaces.Repositories;

public interface IHouseRepository
{
    Task<House?> GetHouseByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<PaginedResult<House>> GetHousesAsync(PaginationParams paginationParams, CancellationToken cancellationToken);
    Task<IEnumerable<House>> GetAllHousesAsync(CancellationToken cancellationToken);
    Task CreateHouseAsync(House house, CancellationToken cancellationToken);
    Task UpdateHouseAsync(House house, CancellationToken cancellationToken);
    Task DeleteHouseAsync(Guid houseId, CancellationToken cancellationToken);
}