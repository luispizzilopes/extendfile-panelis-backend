using ErrorOr;
using ExtendFile.Panelis.Application.Modules.Cat.Responses;
using ExtendFile.Panelis.Application.Modules.Cat.UseCases.GetCatsByBoxId;
using ExtendFile.Panelis.BuildingBlocks.Pagination;
using MediatR;

namespace ExtendFile.Panelis.Application.Modules.Cat.Queries.GetCatsByBoxId;

public class GetCatsByBoxIdQueryHandler : IRequestHandler<GetCatsByBoxIdQuery, ErrorOr<PaginedResult<CatDto>>>
{
    private readonly GetCatsByBoxIdUseCase _getCatsByBoxIdUseCase;

    public GetCatsByBoxIdQueryHandler(GetCatsByBoxIdUseCase getCatsByBoxIdUseCase)
    {
        _getCatsByBoxIdUseCase = getCatsByBoxIdUseCase;
    }

    public async Task<ErrorOr<PaginedResult<CatDto>>> Handle(GetCatsByBoxIdQuery request, CancellationToken cancellationToken)
    {
        return await _getCatsByBoxIdUseCase.ExecuteAsync(request.Request, cancellationToken);
    }
}
