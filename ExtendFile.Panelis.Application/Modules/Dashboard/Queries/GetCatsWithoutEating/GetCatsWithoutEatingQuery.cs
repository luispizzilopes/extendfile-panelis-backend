using ErrorOr;
using ExtendFile.Panelis.Application.Interfaces.Caching;
using ExtendFile.Panelis.Application.Modules.Dashboard.Requests.GetCatsWithoutEating;
using ExtendFile.Panelis.Application.Modules.Dashboard.Responses.GetCatsWithoutEating;
using MediatR;

namespace ExtendFile.Panelis.Application.Modules.Dashboard.Queries.GetCatsWithoutEating;

public record GetCatsWithoutEatingQuery(GetCatsWithoutEatingRequest Request)
    : IRequest<ErrorOr<GetCatsWithoutEatingResponse>>, ICacheableQuery
{
    public string CacheKey => "cats:without-eating";
    public TimeSpan CacheDuration => TimeSpan.FromMinutes(1);
}
