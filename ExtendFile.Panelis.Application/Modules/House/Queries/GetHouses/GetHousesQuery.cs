using ErrorOr;
using ExtendFile.Panelis.Application.Modules.House.Requests.GetHouses;
using ExtendFile.Panelis.Application.Modules.House.Responses;
using ExtendFile.Panelis.BuildingBlocks.Pagination;
using MediatR;

namespace ExtendFile.Panelis.Application.Modules.House.Queries.GetHouses;

public record GetHousesQuery(GetHousesRequest Request) : IRequest<ErrorOr<PaginedResult<HouseDto>>>;
