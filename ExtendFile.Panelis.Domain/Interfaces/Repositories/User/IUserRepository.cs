using ExtendFile.Panelis.BuildingBlocks.Pagination;
using ExtendFile.Panelis.Domain.Modules.User.Entities;

namespace ExtendFile.Panelis.Domain.Interfaces.Repositories.User;

public interface IUserRepository
{
    Task<PaginedResult<Modules.User.Entities.User>> GetUsersAsync(PaginationParams paginationParams, string? name, string? email, CancellationToken cancellationToken);
    Task<Modules.User.Entities.User?> GetByIdAsync(string id, CancellationToken cancellationToken);
    Task<Modules.User.Entities.User?> GetByEmailAsync(string email, CancellationToken cancellationToken);
    Task CreateAsync(Modules.User.Entities.User user, CancellationToken cancellationToken);
    Task UpdateAsync(Modules.User.Entities.User user, CancellationToken cancellationToken);
    Task DeleteAsync(string id, CancellationToken cancellationToken);
}
