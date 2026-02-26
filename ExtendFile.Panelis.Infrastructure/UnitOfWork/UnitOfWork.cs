using ExtendFile.Panelis.Domain.Interfaces.Repositories;
using ExtendFile.Panelis.Domain.Interfaces.Repositories.Cat;
using ExtendFile.Panelis.Domain.Interfaces.UnitOfWork;
using ExtendFile.Panelis.Infrastructure.Context;
using ExtendFile.Panelis.Infrastructure.Repositories.Cat;
using ExtendFile.Panelis.Infrastructure.Repositories.House;
using Microsoft.Extensions.Logging;

namespace ExtendFile.Panelis.Infrastructure.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;
    
    private HouseRepository _houseRepository;
    private readonly ILogger<HouseRepository> _houseLogger;
    
    private CatRepository _catRepository;
    private readonly ILogger<CatRepository> _catLogger;

    public UnitOfWork(
        AppDbContext context,
        ILogger<HouseRepository> houseLogger,
        ILogger<CatRepository> catLogger)
    {
        _context = context;
        _houseLogger = houseLogger;
        _catLogger = catLogger;
    }

    public IHouseRepository HouseRepository
    {
        get
        {
            return _houseRepository = _houseRepository ?? new HouseRepository(_context, _houseLogger);
        }
        set { }
    }

    public ICatRepository CatRepository
    {
        get
        {
            return _catRepository = _catRepository ?? new CatRepository(_context, _catLogger);
        }
        set { }
    }

    public async Task CommitAsync(CancellationToken cancellationToken)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }
}