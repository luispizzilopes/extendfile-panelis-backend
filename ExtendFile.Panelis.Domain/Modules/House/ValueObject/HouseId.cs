using ValueObjectUsing = ExtendFile.Panelis.BuildingBlocks.Common.Class.ValueObject;

namespace ExtendFile.Panelis.Domain.Modules.House.ValueObject;

public class HouseId : ValueObjectUsing.ValueObject
{
    public Guid Value { get; private set; }

    private HouseId(Guid value)
    {
        Value = value;
    }

    public static HouseId CreateIdentifier()
    {
        return new(Guid.NewGuid());
    }

    public static HouseId Create(Guid value)
    {
        return new HouseId(value);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}