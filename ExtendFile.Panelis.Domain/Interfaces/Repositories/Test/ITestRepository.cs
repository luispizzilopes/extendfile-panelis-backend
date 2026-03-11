using ExtendFile.Panelis.BuildingBlocks.Pagination;
using ExtendFile.Panelis.Domain.Modules.Test.Aggregates;
using ExtendFile.Panelis.Domain.Modules.Test.Entities;

namespace ExtendFile.Panelis.Domain.Interfaces.Repositories.Test;

public interface ITestRepository
{
    Task<PaginedResult<Modules.Test.Aggregates.Test>> GetTestsByBoxIdAsync(Guid boxId, PaginationParams paginationParams, CancellationToken cancellationToken);
    Task<IEnumerable<TestLine>?> GetTestLinesByTestIdAsync(Guid testId, CancellationToken cancellationToken);
    Task<Modules.Test.Aggregates.Test?> GetTestByIdAsync(Guid testId, CancellationToken cancellationToken);
    Task<Modules.Test.Aggregates.Test?> GetTestByFileNameAsync(string fileName, CancellationToken cancellationToken);
    Task<Modules.Test.Aggregates.Test?> GetTestByDateAndBoxIdAsync(DateTime testDate, Guid boxId, CancellationToken cancellationToken);
    Task CreateTestAsync(Modules.Test.Aggregates.Test test, CancellationToken cancellationToken);
    Task DeleteTestAsync(Guid testId, CancellationToken cancellationToken);
    
    Task<bool> AnyTestByBoxIdAsync(Guid boxId, CancellationToken cancellationToken);
    Task<Modules.Test.Aggregates.Test?> GetLastTestOrDefaultByBoxIdAsync(Guid boxId, CancellationToken cancellationToken);
}