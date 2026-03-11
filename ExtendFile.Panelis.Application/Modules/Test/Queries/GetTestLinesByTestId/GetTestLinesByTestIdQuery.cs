using ErrorOr;
using ExtendFile.Panelis.Application.Modules.Test.Requests.GetTestLinesByTestId;
using ExtendFile.Panelis.Application.Modules.Test.Responses;
using MediatR;

namespace ExtendFile.Panelis.Application.Modules.Test.Queries.GetTestLinesByTestId;

public record GetTestLinesByTestIdQuery(GetTestLinesByTestIdRequest Request) : IRequest<ErrorOr<List<TestLineDto>>>;
