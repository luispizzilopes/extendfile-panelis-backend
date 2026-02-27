using ErrorOr;
using ExtendFile.Panelis.Application.Modules.Cat.Responses;
using ExtendFile.Panelis.Application.Modules.Cat.UseCases.GetCats;
using ExtendFile.Panelis.BuildingBlocks.Pagination;
using MediatR;

namespace ExtendFile.Panelis.Application.Modules.Cat.Queries.GetCats;

public class GetCatsQueryHandler : IRequestHandler<GetCatsQuery, ErrorOr<PaginedResult<CatDto>>>
{
    private readonly GetCatsUseCase _getCatsUseCase;

    public GetCatsQueryHandler(GetCatsUseCase getCatsUseCase)
    {
        _getCatsUseCase = getCatsUseCase;
    }

    public async Task<ErrorOr<PaginedResult<CatDto>>> Handle(GetCatsQuery request, CancellationToken cancellationToken)
    {
        return await _getCatsUseCase.ExecuteAsync(request.Request, cancellationToken);
    }
}
