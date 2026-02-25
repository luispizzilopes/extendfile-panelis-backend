using ValueObjectUsing = ExtendFile.Panelis.BuildingBlocks.Common.Class.ValueObject;

namespace ExtendFile.Panelis.Domain.Modules.House.ValueObject;

public class BoxId : ValueObjectUsing.ValueObject
{
    public Guid Value { get; private set; }

    private BoxId(Guid value)
    {
        Value = value;
    }

    public static BoxId CreateIdentifier()
    {
        return new(Guid.NewGuid());
    }

    public static BoxId Create(Guid value)
    {
        return new BoxId(value);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}