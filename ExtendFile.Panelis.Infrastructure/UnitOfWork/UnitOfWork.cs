using ExtendFile.Panelis.Application.Modules.Dashboard.Interfaces.Repositories;
using ExtendFile.Panelis.Domain.Interfaces.Repositories;
using ExtendFile.Panelis.Domain.Interfaces.Repositories.Cat;
using ExtendFile.Panelis.Domain.Interfaces.Repositories.Setting;
using ExtendFile.Panelis.Domain.Interfaces.Repositories.Test;
using ExtendFile.Panelis.Domain.Interfaces.Repositories.User;
using ExtendFile.Panelis.Domain.Interfaces.UnitOfWork;
using ExtendFile.Panelis.Infrastructure.Context;
using ExtendFile.Panelis.Infrastructure.Repositories.Cat;
using ExtendFile.Panelis.Infrastructure.Repositories.Dashboard;
using ExtendFile.Panelis.Infrastructure.Repositories.House;
using ExtendFile.Panelis.Infrastructure.Repositories.Setting;
using ExtendFile.Panelis.Infrastructure.Repositories.Test;
using ExtendFile.Panelis.Infrastructure.Repositories.User;
using Microsoft.Extensions.Logging;

namespace ExtendFile.Panelis.Infrastructure.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;
    
    private HouseRepository _houseRepository;
    private readonly ILogger<HouseRepository> _houseLogger;
    
    private CatRepository _catRepository;
    private readonly ILogger<CatRepository> _catLogger;
    
    private TestRepository _testRepository;
    private readonly ILogger<TestRepository> _testLogger;
    
    private SettingRepository _settingRepository;
    private readonly ILogger<SettingRepository> _settingLogger;
    
    private UserRepository _userRepository;
    private readonly ILogger<UserRepository> _userLogger;
    
    private DashboardReadModelRepository  _dashboardReadModelRepository;

    public UnitOfWork(
        AppDbContext context,
        ILogger<HouseRepository> houseLogger,
        ILogger<CatRepository> catLogger,
        ILogger<TestRepository> testLogger,
        ILogger<SettingRepository> settingLogger,
        ILogger<UserRepository> userLogger)
    {
        _context = context;
        _houseLogger = houseLogger;
        _catLogger = catLogger;
        _testLogger = testLogger;
        _settingLogger = settingLogger;
        _userLogger = userLogger;
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

    public IDashboardReadModelRepository DashboardReadModelRepository
    {
        get
        {
            return _dashboardReadModelRepository =
                _dashboardReadModelRepository ?? new DashboardReadModelRepository(_context); 
        }
        set { }
    }

    public ITestRepository TestRepository
    {
        get
        {
            return _testRepository = _testRepository ?? new TestRepository(_context, _testLogger);
        }
        set { }
    }

    public ISettingRepository SettingRepository
    {
        get
        {
            return _settingRepository = _settingRepository ?? new SettingRepository(_context, _settingLogger);
        }
        set { }
    }

    public IUserRepository UserRepository
    {
        get
        {
            return _userRepository = _userRepository ?? new UserRepository(_context, _userLogger);
        }
        set { }
    }
    
    public async Task CommitAsync(CancellationToken cancellationToken)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }
}