using ErrorOr;
using ExtendFile.Panelis.Application.Modules.Test.Responses;
using ExtendFile.Panelis.Application.Modules.Test.UseCases.GetTestLinesByCatId;
using ExtendFile.Panelis.BuildingBlocks.Pagination;
using MediatR;

namespace ExtendFile.Panelis.Application.Modules.Test.Queries.GetTestLinesByCatId;

public class GetTestLinesByCatIdQueryHandler : IRequestHandler<GetTestLinesByCatIdQuery, ErrorOr<PaginedResult<TestLineDto>>>
{
    private readonly GetTestLinesByCatIdUseCase _getTestLinesByCatIdUseCase;

    public GetTestLinesByCatIdQueryHandler(GetTestLinesByCatIdUseCase getTestLinesByCatIdUseCase)
    {
        _getTestLinesByCatIdUseCase = getTestLinesByCatIdUseCase;
    }
    
    public async Task<ErrorOr<PaginedResult<TestLineDto>>> Handle(GetTestLinesByCatIdQuery request, CancellationToken cancellationToken)
    {
        return await _getTestLinesByCatIdUseCase.ExecuteAsync(request.Request, cancellationToken);
    }
}
