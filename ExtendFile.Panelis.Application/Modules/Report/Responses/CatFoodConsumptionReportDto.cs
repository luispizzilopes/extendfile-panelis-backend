namespace ExtendFile.Panelis.Application.Modules.Report.Responses;

public class CatFoodConsumptionReportDto
{
    public string CatName { get; set; } = string.Empty;
    public string CatHash { get; set; } = string.Empty;
    public decimal TotalConsumed { get; set; }
    public decimal AveragePerDay { get; set; }
    public int DaysWithRecords { get; set; }
    public List<CatFoodConsumptionDailyEntryDto> DailyEntries { get; set; } = [];
}
