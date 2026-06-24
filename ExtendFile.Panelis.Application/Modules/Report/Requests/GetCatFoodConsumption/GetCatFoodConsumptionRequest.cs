namespace ExtendFile.Panelis.Application.Modules.Report.Requests.GetCatFoodConsumption;

public class GetCatFoodConsumptionRequest
{
    public Guid CatId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}
