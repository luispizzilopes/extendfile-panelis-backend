using ErrorOr;
using ExtendFile.Panelis.Application.Modules.Test.Requests.GetTestLinesByCatWithoutEating;
using ExtendFile.Panelis.Application.Modules.Test.Responses.GetTestLinesByCatWithoutEating;
using MediatR;

namespace ExtendFile.Panelis.Application.Modules.Test.Queries.GetTestLinesByCatWithoutEating;

public record GetTestLinesByCatWithoutEatingQuery(GetTestLinesByCatWithoutEatingRequest Request) 
    : IRequest<ErrorOr<GetTestLinesByCatWithoutEatingResponse>>;
