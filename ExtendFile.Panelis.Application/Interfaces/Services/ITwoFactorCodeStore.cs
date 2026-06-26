namespace ExtendFile.Panelis.Application.Interfaces.Services;

public interface ITwoFactorCodeStore
{
    Task StoreAsync(string email, string code, CancellationToken cancellationToken = default);
    Task<string?> GetAsync(string email, CancellationToken cancellationToken = default);
    Task RemoveAsync(string email, CancellationToken cancellationToken = default);
}
