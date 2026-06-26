using ExtendFile.Panelis.Application.Interfaces.Services;
using Microsoft.Extensions.Caching.Memory;

namespace ExtendFile.Panelis.Infrastructure.Services;

public class TwoFactorCodeStore : ITwoFactorCodeStore
{
    private readonly IMemoryCache _cache;
    private static readonly TimeSpan Ttl = TimeSpan.FromMinutes(5);

    public TwoFactorCodeStore(IMemoryCache cache)
    {
        _cache = cache;
    }

    public Task StoreAsync(string email, string code, CancellationToken cancellationToken = default)
    {
        _cache.Set(CacheKey(email), code, Ttl);
        return Task.CompletedTask;
    }

    public Task<string?> GetAsync(string email, CancellationToken cancellationToken = default)
    {
        _cache.TryGetValue(CacheKey(email), out string? code);
        return Task.FromResult(code);
    }

    public Task RemoveAsync(string email, CancellationToken cancellationToken = default)
    {
        _cache.Remove(CacheKey(email));
        return Task.CompletedTask;
    }

    private static string CacheKey(string email) => $"2fa:{email.ToLowerInvariant()}";
}
