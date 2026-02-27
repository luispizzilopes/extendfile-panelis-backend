using ErrorOr;
using ExtendFile.Panelis.Application.Modules.Cat.Requests.GetCats;
using ExtendFile.Panelis.Application.Modules.Cat.Responses;
using ExtendFile.Panelis.BuildingBlocks.Pagination;
using MediatR;

namespace ExtendFile.Panelis.Application.Modules.Cat.Queries.GetCats;

public record GetCatsQuery(GetCatsRequest Request) : IRequest<ErrorOr<PaginedResult<CatDto>>>;
