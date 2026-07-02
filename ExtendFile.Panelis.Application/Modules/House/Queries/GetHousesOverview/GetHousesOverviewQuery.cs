using ErrorOr;
using ExtendFile.Panelis.Application.Modules.House.Requests.GetHousesOverview;
using ExtendFile.Panelis.Application.Modules.House.Responses;
using MediatR;

namespace ExtendFile.Panelis.Application.Modules.House.Queries.GetHousesOverview;

public record GetHousesOverviewQuery(GetHousesOverviewRequest Request) : IRequest<ErrorOr<IEnumerable<HouseOverviewDto>>>;
