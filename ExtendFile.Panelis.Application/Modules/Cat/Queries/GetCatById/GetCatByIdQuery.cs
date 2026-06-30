using ErrorOr;
using ExtendFile.Panelis.Application.Interfaces.Caching;
using ExtendFile.Panelis.Application.Modules.Cat.Requests.GetCatById;
using ExtendFile.Panelis.Application.Modules.Cat.Responses;
using MediatR;

namespace ExtendFile.Panelis.Application.Modules.Cat.Queries.GetCatById;

public record GetCatByIdQuery(GetCatByIdRequest Request)
    : IRequest<ErrorOr<CatDto>>, ICacheableQuery
{
    public string CacheKey => $"cat:{Request.Id}";
    public TimeSpan CacheDuration => TimeSpan.FromMinutes(1);
}
