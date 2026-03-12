using ExtendFile.Panelis.Application.Modules.Test.Requests.GetCountDaysWithoutTest;
using ExtendFile.Panelis.Application.Modules.Test.Responses.GetCountDaysWithoutTest;
using ExtendFile.Panelis.Domain.Interfaces.UnitOfWork;

namespace ExtendFile.Panelis.Application.Modules.Test.UseCases.GetCountDaysWithoutTest;

public class GetCountDaysWithoutTestUseCase
{
    private readonly IUnitOfWork _unitOfWork;

    public GetCountDaysWithoutTestUseCase(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<GetCountDaysWithoutTestResponse> ExecuteAsync(GetCountDaysWithoutTestRequest request, CancellationToken cancellationToken = default)
    {
        var days = await _unitOfWork.TestRepository
            .GetCountDaysWithoutTestAsync(request.BoxId, cancellationToken);

        return new GetCountDaysWithoutTestResponse
        {
            Days = days
        };
    }
}