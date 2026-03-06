using ExtendFile.Panelis.BuildingBlocks.Common.Class.Base;
using ExtendFile.Panelis.Domain.Modules.Cat.ValueObject;
using ExtendFile.Panelis.Domain.Modules.Test.Enums;
using ExtendFile.Panelis.Domain.Modules.Test.ValueObject;

namespace ExtendFile.Panelis.Domain.Modules.Test.Entities;

public class TestLine : Entity<TestLineId>
{
    public int Position { get; private set; }
    public string CatName { get; private set; } 
    public CatId CatId { get; private set; }
    public string CatHash { get; private set; }
    public decimal FirstFood { get; private set; }
    public decimal SecondFood { get; private set; }
    public decimal TotalAmountFood { get; private set; }
    public FoodAmountStatus FoodAmountStatus { get; private set; }

    private TestLine(
        TestLineId id,
        int position,
        string catName,
        CatId catId,
        string catHash,
        decimal firstFood,
        decimal secondFood,
        FoodAmountStatus foodAmountStatus) : base(id)
    {
        Position = position;
        CatName = catName;
        CatId = catId;
        CatHash = catHash;
        FirstFood = firstFood;
        SecondFood = secondFood;
        TotalAmountFood = firstFood + secondFood;
        FoodAmountStatus = foodAmountStatus;
    }

    public static TestLine Create(
        int position,
        string catName,
        CatId catId,
        string catHash,
        decimal firstFood,
        decimal secondFood,
        FoodAmountStatus foodAmountStatus)
    {
        var id = TestLineId.CreateIdentifier();

        return new TestLine(
            id,
            position,
            catName,
            catId,
            catHash,
            firstFood,
            secondFood,
            foodAmountStatus);
    }
}