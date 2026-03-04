using ExtendFile.Panelis.BuildingBlocks.Pagination;
using ExtendFile.Panelis.Domain.Interfaces.Repositories;
using ExtendFile.Panelis.Domain.Modules.House.Entities;
using ExtendFile.Panelis.Domain.Modules.House.ValueObject;
using ExtendFile.Panelis.Infrastructure.Context;
using ExtendFile.Panelis.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ExtendFile.Panelis.Infrastructure.Repositories.House;

public class HouseRepository : IHouseRepository
{
    private readonly AppDbContext _context;
    private readonly ILogger<HouseRepository> _logger;

    public HouseRepository(AppDbContext context, ILogger<HouseRepository> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<Domain.Modules.House.Aggregates.House?> GetHouseByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _context.Houses
            .Where(x => x.Id == HouseId.Create(id))
            .Include(x => x.Boxes)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<PaginedResult<Domain.Modules.House.Aggregates.House>> GetHousesAsync(
        PaginationParams paginationParams, 
        CancellationToken cancellationToken)
    {
        return await _context.Houses
            .Include(x => x.Boxes)
            .PaginationAsync(paginationParams, cancellationToken);
    }

    public async Task<IEnumerable<Domain.Modules.House.Aggregates.House>> GetAllHousesAsync(CancellationToken cancellationToken)
    {
        return await _context.Houses
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<Box?> GetBoxByIdAsync(Guid boxId, CancellationToken cancellationToken)
    {
        return await _context.Boxes
            .Where(x => x.Id == BoxId.Create(boxId))
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<Domain.Modules.House.Aggregates.House?> GetHouseByBoxIdAsync(Guid boxId, CancellationToken cancellationToken)
    {
        return await _context.Houses
            .Include(x => x.Boxes)
            .Where(x => x.Boxes.Any(b => b.Id == BoxId.Create(boxId)))
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task CreateHouseAsync(Domain.Modules.House.Aggregates.House house, CancellationToken cancellationToken)
    {
        try
        {
            await _context.Houses.AddAsync(house, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ocorreu um erro ao criar a casa/prédio. Erro: {Message}", ex.Message);
            throw;
        }
    }

    public async Task UpdateHouseAsync(Domain.Modules.House.Aggregates.House house, CancellationToken cancellationToken)
    {
        try
        {
            _context.Houses.Update(house);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ocorreu um erro ao atualizar a casa/prédio. Erro: {Message}", ex.Message);
            throw;
        }
    }

    public async Task DeleteHouseAsync(Guid houseId, CancellationToken cancellationToken)
    {
        try
        {
            var house = await _context.Houses
                .Where(x => x.Id == HouseId.Create(houseId))
                .FirstOrDefaultAsync(cancellationToken);

            if (house is null)
            {
                _logger.LogWarning("Casa/Prédio com id {HouseId} não encontrada.", houseId);
                return;
            }

            _context.Houses.Remove(house);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ocorreu um erro ao excluir a casa/prédio com id {HouseId}. Erro: {Message}", houseId, ex.Message);
            throw;
        }
    }
}