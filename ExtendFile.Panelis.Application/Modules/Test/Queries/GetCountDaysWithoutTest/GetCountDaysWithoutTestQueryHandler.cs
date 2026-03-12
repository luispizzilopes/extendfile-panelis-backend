using ErrorOr;
using ExtendFile.Panelis.Application.Modules.Test.Responses.GetCountDaysWithoutTest;
using ExtendFile.Panelis.Application.Modules.Test.UseCases.GetCountDaysWithoutTest;
using MediatR;

namespace ExtendFile.Panelis.Application.Modules.Test.Queries.GetCountDaysWithoutTest;

public class GetCountDaysWithoutTestQueryHandler : IRequestHandler<GetCountDaysWithoutTestQuery, ErrorOr<GetCountDaysWithoutTestResponse>>
{
    private readonly GetCountDaysWithoutTestUseCase _getCountDaysWithoutTestUseCase;

    public GetCountDaysWithoutTestQueryHandler(GetCountDaysWithoutTestUseCase getCountDaysWithoutTestUseCase)
    {
        _getCountDaysWithoutTestUseCase = getCountDaysWithoutTestUseCase;
    }
    
    public async Task<ErrorOr<GetCountDaysWithoutTestResponse>> Handle(GetCountDaysWithoutTestQuery request, CancellationToken cancellationToken)
    {
        return await _getCountDaysWithoutTestUseCase.ExecuteAsync(request.Request, cancellationToken);
    }
}