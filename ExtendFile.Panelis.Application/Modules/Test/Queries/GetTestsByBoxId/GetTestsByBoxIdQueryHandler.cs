using ErrorOr;
using ExtendFile.Panelis.Application.Modules.Test.Responses;
using ExtendFile.Panelis.Application.Modules.Test.UseCases.GetTestsByBoxId;
using ExtendFile.Panelis.BuildingBlocks.Pagination;
using MediatR;

namespace ExtendFile.Panelis.Application.Modules.Test.Queries.GetTestsByBoxId;

public class GetTestsByBoxIdQueryHandler : IRequestHandler<GetTestsByBoxIdQuery, ErrorOr<PaginedResult<TestDto>>>
{
    private readonly GetTestsByBoxIdUseCase _getTestsByBoxIdUseCase;

    public GetTestsByBoxIdQueryHandler(GetTestsByBoxIdUseCase getTestsByBoxIdUseCase)
    {
        _getTestsByBoxIdUseCase = getTestsByBoxIdUseCase;
    }

    public async Task<ErrorOr<PaginedResult<TestDto>>> Handle(GetTestsByBoxIdQuery request, CancellationToken cancellationToken)
    {
        return await _getTestsByBoxIdUseCase.ExecuteAsync(request.Request, cancellationToken);
    }
}
