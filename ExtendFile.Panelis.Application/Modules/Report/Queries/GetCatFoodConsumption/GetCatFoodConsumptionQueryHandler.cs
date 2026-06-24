using ErrorOr;
using ExtendFile.Panelis.Application.Modules.Report.Responses;
using ExtendFile.Panelis.Application.Modules.Report.UseCases.GetCatFoodConsumption;
using MediatR;

namespace ExtendFile.Panelis.Application.Modules.Report.Queries.GetCatFoodConsumption;

public class GetCatFoodConsumptionQueryHandler : IRequestHandler<GetCatFoodConsumptionQuery, ErrorOr<CatFoodConsumptionReportDto>>
{
    private readonly GetCatFoodConsumptionUseCase _useCase;

    public GetCatFoodConsumptionQueryHandler(GetCatFoodConsumptionUseCase useCase)
    {
        _useCase = useCase;
    }

    public async Task<ErrorOr<CatFoodConsumptionReportDto>> Handle(GetCatFoodConsumptionQuery request, CancellationToken cancellationToken)
    {
        return await _useCase.ExecuteAsync(request.Request, cancellationToken);
    }
}
