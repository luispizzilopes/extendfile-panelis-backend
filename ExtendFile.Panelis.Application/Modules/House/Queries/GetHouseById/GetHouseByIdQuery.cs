using ErrorOr;
using ExtendFile.Panelis.Application.Interfaces.Caching;
using ExtendFile.Panelis.Application.Modules.House.Requests.GetHouseById;
using ExtendFile.Panelis.Application.Modules.House.Responses;
using MediatR;

namespace ExtendFile.Panelis.Application.Modules.House.Queries.GetHouseById;

public record GetHouseByIdQuery(GetHouseByIdRequest Request)
    : IRequest<ErrorOr<HouseDto>>, ICacheableQuery
{
    public string CacheKey => $"house:{Request.Id}";
    public TimeSpan CacheDuration => TimeSpan.FromMinutes(1);
} 