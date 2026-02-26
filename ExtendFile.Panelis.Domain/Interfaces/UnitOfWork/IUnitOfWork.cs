using ExtendFile.Panelis.Domain.Interfaces.Repositories;
using ExtendFile.Panelis.Domain.Interfaces.Repositories.Cat;

namespace ExtendFile.Panelis.Domain.Interfaces.UnitOfWork;

public interface IUnitOfWork
{
    IHouseRepository HouseRepository { get; set; }
    ICatRepository CatRepository { get; set; }
    
    Task CommitAsync(CancellationToken cancellationToken);
}