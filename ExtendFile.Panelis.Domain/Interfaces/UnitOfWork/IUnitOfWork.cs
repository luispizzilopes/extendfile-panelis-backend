using ExtendFile.Panelis.Domain.Interfaces.Repositories;
using ExtendFile.Panelis.Domain.Interfaces.Repositories.Cat;
using ExtendFile.Panelis.Domain.Interfaces.Repositories.Setting;
using ExtendFile.Panelis.Domain.Interfaces.Repositories.Test;

namespace ExtendFile.Panelis.Domain.Interfaces.UnitOfWork;

public interface IUnitOfWork
{
    IHouseRepository HouseRepository { get; set; }
    ICatRepository CatRepository { get; set; }
    ITestRepository TestRepository { get; set; }
    ISettingRepository SettingRepository { get; set; }
    
    Task CommitAsync(CancellationToken cancellationToken);
}