namespace ExtendFile.Panelis.Application.Interfaces.Services;

public record PasswordResetEntry(string Code, string IdentityToken);

public interface IPasswordResetCodeStore
{
    Task StoreAsync(string email, string code, string identityToken, CancellationToken cancellationToken = default);
    Task<PasswordResetEntry?> GetAsync(string email, CancellationToken cancellationToken = default);
    Task RemoveAsync(string email, CancellationToken cancellationToken = default);
}
