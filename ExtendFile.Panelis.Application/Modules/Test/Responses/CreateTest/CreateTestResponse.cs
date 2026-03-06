using ExtendFile.Panelis.Domain.Modules.Test.Enums;

namespace ExtendFile.Panelis.Application.Modules.Test.Responses.CreateTest;

public class CreateTestResponse
{
    public TestResponse TestResponse { get; set; } = null!; 
}

public class TestResponse
{
    public DateTime TestDate { get; set; }
    public List<TestLineResponse> Lines { get; set; } = null!;
}

public class TestLineResponse
{
    public string CatName { get; set; } = null!; 
    public string CatHash { get; set; } = null!;
    public decimal FirstFood { get; set; }
    public decimal SecondFood { get; set; }
    public decimal TotalAmountFood { get; set; }
    public FoodAmountStatus FoodAmountStatus { get; set; }
}