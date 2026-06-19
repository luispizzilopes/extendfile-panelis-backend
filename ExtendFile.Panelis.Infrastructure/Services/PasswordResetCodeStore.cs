using ExtendFile.Panelis.Application.Interfaces.Services;
using Microsoft.Extensions.Caching.Memory;

namespace ExtendFile.Panelis.Infrastructure.Services;

public class PasswordResetCodeStore : IPasswordResetCodeStore
{
    private readonly IMemoryCache _cache;
    private static readonly TimeSpan Ttl = TimeSpan.FromMinutes(15);

    public PasswordResetCodeStore(IMemoryCache cache)
    {
        _cache = cache;
    }

    public Task StoreAsync(string email, string code, string identityToken, CancellationToken cancellationToken = default)
    {
        var entry = new PasswordResetEntry(code, identityToken);
        _cache.Set(CacheKey(email), entry, Ttl);
        return Task.CompletedTask;
    }

    public Task<PasswordResetEntry?> GetAsync(string email, CancellationToken cancellationToken = default)
    {
        _cache.TryGetValue(CacheKey(email), out PasswordResetEntry? entry);
        return Task.FromResult(entry);
    }

    public Task RemoveAsync(string email, CancellationToken cancellationToken = default)
    {
        _cache.Remove(CacheKey(email));
        return Task.CompletedTask;
    }

    private static string CacheKey(string email) => $"pwd_reset:{email.ToLowerInvariant()}";
}
