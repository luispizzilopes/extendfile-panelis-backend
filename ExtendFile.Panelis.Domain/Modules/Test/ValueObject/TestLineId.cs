namespace ExtendFile.Panelis.Domain.Modules.Test.ValueObject;

public class TestLineId : BuildingBlocks.Common.Class.ValueObject.ValueObject
{
    public Guid Value { get; private set; }

    private TestLineId(Guid value)
    {
        Value = value;
    }

    public static TestLineId CreateIdentifier()
    {
        return new(Guid.NewGuid());
    }

    public static TestLineId Create(Guid value)
    {
        return new TestLineId(value);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}