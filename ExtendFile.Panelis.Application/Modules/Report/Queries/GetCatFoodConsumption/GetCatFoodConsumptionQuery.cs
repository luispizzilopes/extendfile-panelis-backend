using ErrorOr;
using ExtendFile.Panelis.Application.Modules.Report.Requests.GetCatFoodConsumption;
using ExtendFile.Panelis.Application.Modules.Report.Responses;
using MediatR;

namespace ExtendFile.Panelis.Application.Modules.Report.Queries.GetCatFoodConsumption;

public record GetCatFoodConsumptionQuery(GetCatFoodConsumptionRequest Request) : IRequest<ErrorOr<CatFoodConsumptionReportDto>>;
