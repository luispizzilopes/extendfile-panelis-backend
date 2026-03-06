using ExtendFile.Panelis.Domain.Modules.Setting.Aggregates;

namespace ExtendFile.Panelis.Domain.Interfaces.Repositories.Setting;

public interface ISettingRepository
{
    Task<Modules.Setting.Aggregates.Setting?> GetSettingAsync(CancellationToken cancellationToken);
    Task CreateSettingAsync(Modules.Setting.Aggregates.Setting setting, CancellationToken cancellationToken);
    Task UpdateSettingAsync(Modules.Setting.Aggregates.Setting setting, CancellationToken cancellationToken);
}
