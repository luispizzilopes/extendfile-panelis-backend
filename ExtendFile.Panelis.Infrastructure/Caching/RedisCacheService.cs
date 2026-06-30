using System.Text.Json;
using ExtendFile.Panelis.Domain.Interfaces.Caching;
using Microsoft.Extensions.Configuration;
using StackExchange.Redis;

namespace ExtendFile.Panelis.Infrastructure.Caching;

public class RedisCacheService : ICacheService
{
    private readonly IConnectionMultiplexer _redis;
    private readonly IDatabase _database;
    private readonly string _instanceName;

    public RedisCacheService(
        IConnectionMultiplexer redis, 
        IConfiguration configuration)
    {
        _redis = redis;
        _database = redis.GetDatabase();
        _instanceName = configuration["Redis:InstanceName"] ?? string.Empty;
    }

    public async Task<T?> GetAsync<T>(string key, CancellationToken cancellationToken = default)
    {
        var value = await _database.StringGetAsync(GetKey(key));
        
        if (!value.HasValue)
            return default;

        return JsonSerializer.Deserialize<T>(value!);
    }

    public async Task SetAsync<T>(string key, T value, TimeSpan? expiration = null, CancellationToken cancellationToken = default)
    {
        var serializedValue = JsonSerializer.Serialize(value);
        var expirationValue = expiration.HasValue ? new Expiration(expiration.Value) : Expiration.Default;
        await _database.StringSetAsync(GetKey(key), serializedValue, expirationValue);
    }

    public async Task RemoveAsync(string key, CancellationToken cancellationToken = default)
    {
        await _database.KeyDeleteAsync(GetKey(key));
    }
    
    public async Task<bool> ExistsAsync(string key, CancellationToken cancellationToken = default)
    {
        return await _database.KeyExistsAsync(GetKey(key));
    }

    public async Task RemoveByPrefixAsync(string prefix, CancellationToken cancellationToken = default)
    {
        var endpoints = _redis.GetEndPoints();

        foreach (var endpoint in endpoints)
        {
            var server = _redis.GetServer(endpoint);

            if (!server.IsReplica)
            {
                var keys = server.Keys(pattern: $"{GetKey(prefix)}*");

                foreach (var key in keys)
                {
                    await _database.KeyDeleteAsync(key);
                }
            }
        }
    }

    public async Task<long> GetUsedMemoryBytesAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            var server = _redis.GetServer(_redis.GetEndPoints().First());
            var info = await server.InfoAsync("memory");
            var entry = info.SelectMany(g => g).FirstOrDefault(x => x.Key == "used_memory");
            return long.TryParse(entry.Value, out var bytes) ? bytes : long.MaxValue;
        }
        catch
        {
            return 0;
        }
    }

    private string GetKey(string key) => $"{_instanceName}{key}";
}