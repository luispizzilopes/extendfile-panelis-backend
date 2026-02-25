using ValueObjectUsing = ExtendFile.Panelis.BuildingBlocks.Common.Class.ValueObject;

namespace ExtendFile.Panelis.Domain.Modules.Cat.ValueObject;

public class CatId : ValueObjectUsing.ValueObject
{
    public Guid Value { get; private set; }

    private CatId(Guid value)
    {
        Value = value;
    }

    public static CatId CreateIdentifier()
    {
        return new(Guid.NewGuid());
    }

    public static CatId Create(Guid value)
    {
        return new CatId(value);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}