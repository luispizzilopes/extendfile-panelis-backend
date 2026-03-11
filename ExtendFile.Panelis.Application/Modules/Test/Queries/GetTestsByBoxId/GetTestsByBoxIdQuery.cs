using ErrorOr;
using ExtendFile.Panelis.Application.Modules.Test.Requests.GetTestsByBoxId;
using ExtendFile.Panelis.Application.Modules.Test.Responses;
using ExtendFile.Panelis.BuildingBlocks.Pagination;
using MediatR;

namespace ExtendFile.Panelis.Application.Modules.Test.Queries.GetTestsByBoxId;

public record GetTestsByBoxIdQuery(GetTestsByBoxIdRequest Request) : IRequest<ErrorOr<PaginedResult<TestDto>>>;
