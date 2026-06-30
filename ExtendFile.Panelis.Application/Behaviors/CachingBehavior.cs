using System.Text.Json;
using ErrorOr;
using ExtendFile.Panelis.Application.Interfaces.Caching;
using ExtendFile.Panelis.Domain.Interfaces.Caching;
using MediatR;
using Microsoft.Extensions.Hosting;

namespace ExtendFile.Panelis.Application.Behaviors;

public class CachingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private const long MaxMemoryBytes = 30 * 1024 * 1024;
    private const double SafetyMargin = 0.10;

    private readonly ICacheService _cacheService;
    private readonly IHostEnvironment _environment;

    public CachingBehavior(ICacheService cacheService, IHostEnvironment environment)
    {
        _cacheService = cacheService;
        _environment = environment;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (request is not ICacheableQuery cacheableQuery || !_environment.IsProduction())
            return await next(cancellationToken);

        var cacheKey = cacheableQuery.CacheKey;

        var cachedJson = await _cacheService.GetAsync<string>(cacheKey, cancellationToken);
        if (cachedJson is not null)
        {
            var deserialized = DeserializeResponse(cachedJson);
            if (deserialized is not null)
                return deserialized;
        }

        var response = await next(cancellationToken);

        var json = SerializeResponse(response);
        if (json is not null)
        {
            var usedBytes = await _cacheService.GetUsedMemoryBytesAsync(cancellationToken);
            if (HasAvailableMemory(usedBytes))
                await _cacheService.SetAsync(cacheKey, json, cacheableQuery.CacheDuration, cancellationToken);
        }

        return response;
    }

    private TResponse? DeserializeResponse(string json)
    {
        var innerType = GetErrorOrValueType(typeof(TResponse));
        if (innerType is null)
            return JsonSerializer.Deserialize<TResponse>(json);

        var innerValue = JsonSerializer.Deserialize(json, innerType);
        if (innerValue is null) return default;

        // reconstrói ErrorOr<T> via conversão implícita de TValue → ErrorOr<TValue>
        return (TResponse)(dynamic)innerValue;
    }

    private static string? SerializeResponse(TResponse response)
    {
        var innerType = GetErrorOrValueType(typeof(TResponse));
        if (innerType is null)
            return JsonSerializer.Serialize(response);

        dynamic dynResponse = response!;
        if (dynResponse.IsError) return null;

        return JsonSerializer.Serialize(dynResponse.Value, innerType);
    }

    // retorna o TValue de ErrorOr<TValue>, ou null se TResponse não for ErrorOr
    private static Type? GetErrorOrValueType(Type type) =>
        type.IsGenericType && type.GetGenericTypeDefinition() == typeof(ErrorOr<>)
            ? type.GetGenericArguments()[0]
            : null;

    private static bool HasAvailableMemory(long usedBytes) =>
        usedBytes < MaxMemoryBytes * (1 - SafetyMargin);
}
