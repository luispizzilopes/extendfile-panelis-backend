using ExtendFile.Panelis.Application.Modules.Report.Responses;

namespace ExtendFile.Panelis.Application.Modules.Report.Interfaces.Repositories;

public interface IReportRepository
{
    Task<IEnumerable<CatFoodConsumptionDailyEntryDto>> GetCatFoodConsumptionAsync(
        Guid catId,
        DateTime startDate,
        DateTime endDate,
        CancellationToken cancellationToken);
}
