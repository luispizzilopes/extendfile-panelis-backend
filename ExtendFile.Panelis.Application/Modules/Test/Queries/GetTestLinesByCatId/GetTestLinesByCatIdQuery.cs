using ErrorOr;
using ExtendFile.Panelis.Application.Modules.Test.Requests.GetTestLinesByCatId;
using ExtendFile.Panelis.Application.Modules.Test.Responses;
using ExtendFile.Panelis.BuildingBlocks.Pagination;
using MediatR;

namespace ExtendFile.Panelis.Application.Modules.Test.Queries.GetTestLinesByCatId;

public record GetTestLinesByCatIdQuery(GetTestLinesByCatIdRequest Request) 
    : IRequest<ErrorOr<PaginedResult<TestLineDto>>>;
