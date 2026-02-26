using ErrorOr;
using ExtendFile.Panelis.Application.Modules.House.Responses;
using MediatR;

namespace ExtendFile.Panelis.Application.Modules.House.Queries.GetAllHouses;

public record GetAllHousesQuery() : IRequest<ErrorOr<IEnumerable<HouseDto>>>;
