using ExtendFile.Panelis.Domain.Modules.Test.Enums;

namespace ExtendFile.Panelis.Application.Modules.Test.Responses;

public class TestLineDto
{
    public Guid Id { get; set; }
    public int Position { get; set; }
    public string CatName { get; set; } = string.Empty;
    public Guid CatId { get; set; }
    public string CatHash { get; set; } = string.Empty;
    public decimal FirstFood { get; set; }
    public decimal SecondFood { get; set; }
    public decimal TotalAmountFood { get; set; }
    public FoodAmountStatus FoodAmountStatus { get; set; }
}
