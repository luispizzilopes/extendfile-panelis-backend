using ExtendFile.Panelis.BuildingBlocks.Pagination;
using ExtendFile.Panelis.Domain.Interfaces.Repositories.Cat;
using ExtendFile.Panelis.Domain.Modules.Cat.ValueObject;
using ExtendFile.Panelis.Domain.Modules.House.ValueObject;
using ExtendFile.Panelis.Infrastructure.Context;
using ExtendFile.Panelis.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Aggregates = ExtendFile.Panelis.Domain.Modules.Cat.Aggregates;

namespace ExtendFile.Panelis.Infrastructure.Repositories.Cat;

public class CatRepository : ICatRepository
{
    private readonly AppDbContext _context;
    private readonly ILogger<CatRepository> _logger;

    public CatRepository(AppDbContext context, ILogger<CatRepository> logger)
    {
        _context = context;
        _logger = logger;
    }
    
    public async Task<Aggregates.Cat?> GetCatByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _context.Cats
            .Where(x => x.Id == CatId.Create(id))
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<PaginedResult<Aggregates.Cat>> GetCatsByBoxIdAsync(
        PaginationParams paginationParams, 
        Guid boxId, 
        CancellationToken cancellationToken)
    {
        return await _context.Cats
            .Where(x => x.BoxId == BoxId.Create(boxId))
            .PaginationAsync(paginationParams, cancellationToken);
    }

    public async Task<PaginedResult<Aggregates.Cat>> GetCatsAsync(PaginationParams paginationParams, CancellationToken cancellationToken)
    {
        return await _context.Cats.PaginationAsync(paginationParams, cancellationToken);
    }

    public async Task<IEnumerable<Aggregates.Cat>> GetAllCatsAsync(CancellationToken cancellationToken)
    {
        return await _context.Cats
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task CreateCatAsync(Aggregates.Cat cat, CancellationToken cancellationToken)
    {
        try
        {
            await _context.Cats.AddAsync(cat, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ocorreu um erro ao criar o gato. Erro: {Message}", ex.Message);
            throw;
        }
    }

    public async Task UpdateCatAsync(Aggregates.Cat cat, CancellationToken cancellationToken)
    {
        try
        {
            _context.Cats.Update(cat);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ocorreu um erro ao atualizar o gato. Erro: {Message}", ex.Message);
            throw;
        }
    }

    public async Task DeleteCatAsync(Guid catId, CancellationToken cancellationToken)
    {
        try
        {
            var cat = await _context.Cats
                .Where(x => x.Id == CatId.Create(catId))
                .FirstOrDefaultAsync(cancellationToken);

            if (cat is null)
            {
                _logger.LogWarning("Gato com id {CatId} não encontrado.", catId);
                return;
            }

            _context.Cats.Remove(cat);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ocorreu um erro ao excluir o gato com id {CatId}. Erro: {Message}", catId, ex.Message);
            throw;
        }
    }
}