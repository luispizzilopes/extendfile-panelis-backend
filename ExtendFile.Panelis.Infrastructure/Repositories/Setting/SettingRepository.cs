using ExtendFile.Panelis.Domain.Interfaces.Repositories.Setting;
using ExtendFile.Panelis.Domain.Modules.Setting.Aggregates;
using ExtendFile.Panelis.Domain.Modules.Setting.ValueObject;
using ExtendFile.Panelis.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ExtendFile.Panelis.Infrastructure.Repositories.Setting;

public class SettingRepository : ISettingRepository
{
    private readonly AppDbContext _context;
    private readonly ILogger<SettingRepository> _logger;

    public SettingRepository(AppDbContext context, ILogger<SettingRepository> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<Domain.Modules.Setting.Aggregates.Setting?> GetSettingAsync(CancellationToken cancellationToken)
    {
        try
        {
            return await _context.Settings
                .AsNoTracking()
                .FirstOrDefaultAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ocorreu um erro ao buscar as configurações. Erro: {Message}", ex.Message);
            throw;
        }
    }

    public async Task CreateSettingAsync(Domain.Modules.Setting.Aggregates.Setting setting, CancellationToken cancellationToken)
    {
        try
        {
            await _context.Settings.AddAsync(setting, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ocorreu um erro ao criar as configurações. Erro: {Message}", ex.Message);
            throw;
        }
    }

    public async Task UpdateSettingAsync(Domain.Modules.Setting.Aggregates.Setting setting, CancellationToken cancellationToken)
    {
        try
        {
            _context.Settings.Update(setting);
            await Task.CompletedTask;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ocorreu um erro ao atualizar as configurações. Erro: {Message}", ex.Message);
            throw;
        }
    }
}
