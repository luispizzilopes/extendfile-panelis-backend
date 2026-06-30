using ErrorOr;
using ExtendFile.Panelis.Application.Interfaces.Caching;
using ExtendFile.Panelis.Application.Modules.Cat.Requests.GetCatsByBoxId;
using ExtendFile.Panelis.Application.Modules.Cat.Responses;
using ExtendFile.Panelis.BuildingBlocks.Pagination;
using MediatR;

namespace ExtendFile.Panelis.Application.Modules.Cat.Queries.GetCatsByBoxId;

public record GetCatsByBoxIdQuery(GetCatsByBoxIdRequest Request)
    : IRequest<ErrorOr<PaginedResult<CatDto>>>, ICacheableQuery
{
    public string CacheKey => $"cats:box:{Request.BoxId}:p{Request.PaginationParams.PageNumber}:s{Request.PaginationParams.PageSize}";
    public TimeSpan CacheDuration => TimeSpan.FromMinutes(1);
}
