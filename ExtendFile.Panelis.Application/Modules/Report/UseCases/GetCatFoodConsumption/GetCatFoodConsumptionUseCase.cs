using ErrorOr;
using ExtendFile.Panelis.Application.Modules.Report.Interfaces.Repositories;
using ExtendFile.Panelis.Application.Modules.Report.Requests.GetCatFoodConsumption;
using ExtendFile.Panelis.Application.Modules.Report.Responses;
using ExtendFile.Panelis.Domain.Interfaces.UnitOfWork;

namespace ExtendFile.Panelis.Application.Modules.Report.UseCases.GetCatFoodConsumption;

public class GetCatFoodConsumptionUseCase
{
    private readonly IReportRepository _reportRepository;
    private readonly IUnitOfWork _unitOfWork;

    public GetCatFoodConsumptionUseCase(IReportRepository reportRepository, IUnitOfWork unitOfWork)
    {
        _reportRepository = reportRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<CatFoodConsumptionReportDto>> ExecuteAsync(
        GetCatFoodConsumptionRequest request,
        CancellationToken cancellationToken = default)
    {
        var cat = await _unitOfWork.CatRepository.GetCatByIdAsync(request.CatId, cancellationToken);

        if (cat is null)
            return Error.NotFound("Cat.NotFound", $"Gato com ID '{request.CatId}' não encontrado.");

        var entries = (await _reportRepository.GetCatFoodConsumptionAsync(
            request.CatId,
            request.StartDate,
            request.EndDate,
            cancellationToken)).ToList();

        var totalConsumed = entries.Sum(e => e.TotalAmountFood);
        var daysWithRecords = entries.Count;
        var averagePerDay = daysWithRecords > 0
            ? Math.Round(totalConsumed / daysWithRecords, 2)
            : 0;

        return new CatFoodConsumptionReportDto
        {
            CatName = cat.Name,
            CatHash = cat.Hash,
            TotalConsumed = totalConsumed,
            AveragePerDay = averagePerDay,
            DaysWithRecords = daysWithRecords,
            DailyEntries = entries
        };
    }
}
