using ErrorOr;
using ExtendFile.Panelis.Application.Modules.Cat.Requests.GetCatsByBoxId;
using ExtendFile.Panelis.Application.Modules.Cat.Responses;
using ExtendFile.Panelis.BuildingBlocks.Pagination;
using MediatR;

namespace ExtendFile.Panelis.Application.Modules.Cat.Queries.GetCatsByBoxId;

public record GetCatsByBoxIdQuery(GetCatsByBoxIdRequest Request) : IRequest<ErrorOr<PaginedResult<CatDto>>>;
