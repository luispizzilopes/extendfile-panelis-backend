using ExtendFile.Panelis.BuildingBlocks.Pagination;
using ExtendFile.Panelis.Domain.Interfaces.Repositories.Test;
using ExtendFile.Panelis.Domain.Modules.House.ValueObject;
using ExtendFile.Panelis.Domain.Modules.Test.Entities;
using ExtendFile.Panelis.Domain.Modules.Test.ValueObject;
using ExtendFile.Panelis.Infrastructure.Context;
using ExtendFile.Panelis.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ExtendFile.Panelis.Infrastructure.Repositories.Test;

public class TestRepository : ITestRepository
{
    private readonly AppDbContext _context;
    private readonly ILogger<TestRepository> _logger;

    public TestRepository(AppDbContext context, ILogger<TestRepository> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<PaginedResult<Domain.Modules.Test.Aggregates.Test>> GetTestsByBoxIdAsync(Guid boxId, PaginationParams paginationParams, CancellationToken cancellationToken)
    {
        try
        {
            return await _context.Tests
                .Where(x => x.BoxId.Value == boxId)
                .Include(x => x.Lines)
                .PaginationAsync(paginationParams, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ocorreu um erro ao buscar os testes por BoxId. Erro: {Message}", ex.Message);
            throw;
        }
    }

    public async Task<IEnumerable<TestLine>> GetTestLinesByTestIdAsync(Guid testId, CancellationToken cancellationToken)
    {
        try
        {
            return await _context.TestLines
                .Where(x => EF.Property<Guid>(x, "TestId") == testId)
                .OrderBy(x => x.Position)
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ocorreu um erro ao buscar as linhas do teste. Erro: {Message}", ex.Message);
            throw;
        }
    }

    public async Task<Domain.Modules.Test.Aggregates.Test?> GetTestByFileNameAsync(string fileName, CancellationToken cancellationToken)
    {
        try
        {
            return await _context.Tests
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.FileName == fileName, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ocorreu um erro ao buscar o teste pelo nome do arquivo '{FileName}'. Erro: {Message}", fileName, ex.Message);
            throw;
        }
    }

    public async Task<Domain.Modules.Test.Aggregates.Test?> GetTestByDateAndBoxIdAsync(DateTime testDate, Guid boxId, CancellationToken cancellationToken)
    {
        try
        {
            var dateStart = testDate.Date.ToUniversalTime();
            var dateEnd = dateStart.AddDays(1);

            return await _context.Tests
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.TestDate >= dateStart && x.TestDate < dateEnd && x.BoxId == BoxId.Create(boxId), cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ocorreu um erro ao buscar o teste pela data '{TestDate}' e BoxId '{BoxId}'. Erro: {Message}", testDate, boxId, ex.Message);
            throw;
        }
    }

    public async Task CreateTestAsync(Domain.Modules.Test.Aggregates.Test test, CancellationToken cancellationToken)
    {
        try
        {
            await _context.Tests.AddAsync(test, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ocorreu um erro ao criar o teste. Erro: {Message}", ex.Message);
            throw;
        }
    }

    public async Task DeleteTestAsync(Guid testId, CancellationToken cancellationToken)
    {
        try
        {
            var test = await _context.Tests
                .Where(x => x.Id == TestId.Create(testId))
                .FirstOrDefaultAsync(cancellationToken);

            if (test is null)
            {
                _logger.LogWarning("Teste com id {TestId} não encontrado.", testId);
                return;
            }

            _context.Tests.Remove(test);
            await _context.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ocorreu um erro ao excluir o teste com id {TestId}. Erro: {Message}", testId, ex.Message);
            throw;
        }
    }
}