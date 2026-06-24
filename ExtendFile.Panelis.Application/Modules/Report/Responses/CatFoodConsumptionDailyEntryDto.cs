using ExtendFile.Panelis.Domain.Modules.Test.Enums;

namespace ExtendFile.Panelis.Application.Modules.Report.Responses;

public class CatFoodConsumptionDailyEntryDto
{
    public DateTime TestDate { get; set; }
    public decimal FirstFood { get; set; }
    public decimal SecondFood { get; set; }
    public decimal TotalAmountFood { get; set; }
    public FoodAmountStatus FoodAmountStatus { get; set; }
}
