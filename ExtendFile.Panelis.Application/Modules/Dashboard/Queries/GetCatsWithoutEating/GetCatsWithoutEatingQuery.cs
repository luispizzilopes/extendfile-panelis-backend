using ErrorOr;
using ExtendFile.Panelis.Application.Modules.Dashboard.Requests.GetCatsWithoutEating;
using ExtendFile.Panelis.Application.Modules.Dashboard.Responses.GetCatsWithoutEating;
using MediatR;

namespace ExtendFile.Panelis.Application.Modules.Dashboard.Queries.GetCatsWithoutEating;

public record GetCatsWithoutEatingQuery(GetCatsWithoutEatingRequest Request) : IRequest<ErrorOr<GetCatsWithoutEatingResponse>>;
