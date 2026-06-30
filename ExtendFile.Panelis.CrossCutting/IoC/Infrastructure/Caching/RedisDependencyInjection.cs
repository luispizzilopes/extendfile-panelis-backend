using ExtendFile.Panelis.Domain.Interfaces.Caching;
using ExtendFile.Panelis.Infrastructure.Caching;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StackExchange.Redis;

namespace ExtendFile.Panelis.CrossCutting.IoC.Infrastructure.Caching;

public static class RedisDependencyInjection
{
    public static void AddRedisCache(
        this IServiceCollection services,
        IConfiguration configuration,
        IWebHostEnvironment environment)
    {
        if (!environment.IsProduction())
        {
            services.AddSingleton<ICacheService, NullCacheService>();
            return;
        }

        var redisPassword = Environment.GetEnvironmentVariable("REDIS_PASSWORD");

        services.AddSingleton<IConnectionMultiplexer>(sp =>
        {
            var redisHost = configuration["Redis:Host"];
            var redisPort = configuration["Redis:Port"];
            var redisConnection = $"{redisHost}:{redisPort}";
            var options = ConfigurationOptions.Parse(redisConnection!);
            options.Password = redisPassword;
            options.AllowAdmin = true;
            options.ConnectRetry = 5;
            options.ConnectTimeout = 15000;
            options.SyncTimeout = 5000;
            options.ReconnectRetryPolicy = new ExponentialRetry(5000);
            options.KeepAlive = 60;
            options.AbortOnConnectFail = false;
            options.ResolveDns = true;
            options.Ssl = false;
            return ConnectionMultiplexer.Connect(options);
        });

        services.AddSingleton<ICacheService, RedisCacheService>();
    }
}