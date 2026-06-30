using ErrorOr;
using ExtendFile.Panelis.Application.Interfaces.Caching;
using ExtendFile.Panelis.Application.Modules.Test.Requests.GetCountDaysWithoutTest;
using ExtendFile.Panelis.Application.Modules.Test.Responses.GetCountDaysWithoutTest;
using MediatR;

namespace ExtendFile.Panelis.Application.Modules.Test.Queries.GetCountDaysWithoutTest;

public record GetCountDaysWithoutTestQuery(GetCountDaysWithoutTestRequest Request)
    : IRequest<ErrorOr<GetCountDaysWithoutTestResponse>>, ICacheableQuery
{
    public string CacheKey => $"test:days-without:{Request.BoxId}";
    public TimeSpan CacheDuration => TimeSpan.FromMinutes(1);
}