namespace ExtendFile.Panelis.Application.Interfaces.Caching;

public interface ICacheableQuery
{
    string CacheKey { get; }
    TimeSpan CacheDuration { get; }
}
