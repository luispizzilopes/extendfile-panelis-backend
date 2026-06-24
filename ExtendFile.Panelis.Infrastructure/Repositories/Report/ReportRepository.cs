using ExtendFile.Panelis.Application.Modules.Report.Interfaces.Repositories;
using ExtendFile.Panelis.Application.Modules.Report.Responses;
using ExtendFile.Panelis.Domain.Modules.Cat.ValueObject;
using ExtendFile.Panelis.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace ExtendFile.Panelis.Infrastructure.Repositories.Report;

public class ReportRepository : IReportRepository
{
    private readonly AppDbContext _context;

    public ReportRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<CatFoodConsumptionDailyEntryDto>> GetCatFoodConsumptionAsync(
        Guid catId,
        DateTime startDate,
        DateTime endDate,
        CancellationToken cancellationToken)
    {
        var catIdValue = CatId.Create(catId);

        var entries = await _context.Tests
            .Where(t => t.TestDate.Date >= startDate.Date && t.TestDate.Date <= endDate.Date)
            .SelectMany(t => t.Lines
                .Where(tl => tl.CatId == catIdValue)
                .Select(tl => new CatFoodConsumptionDailyEntryDto
                {
                    TestDate = t.TestDate,
                    FirstFood = tl.FirstFood,
                    SecondFood = tl.SecondFood,
                    TotalAmountFood = tl.TotalAmountFood,
                    FoodAmountStatus = tl.FoodAmountStatus
                }))
            .OrderBy(x => x.TestDate)
            .ToListAsync(cancellationToken);

        return entries;
    }
}
