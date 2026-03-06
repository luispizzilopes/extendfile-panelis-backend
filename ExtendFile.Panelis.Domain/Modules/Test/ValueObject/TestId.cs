using ExtendFile.Panelis.Domain.Modules.Cat.ValueObject;

namespace ExtendFile.Panelis.Domain.Modules.Test.ValueObject;

public class TestId : BuildingBlocks.Common.Class.ValueObject.ValueObject
{
    public Guid Value { get; private set; }

    private TestId(Guid value)
    {
        Value = value;
    }

    public static TestId CreateIdentifier()
    {
        return new(Guid.NewGuid());
    }

    public static TestId Create(Guid value)
    {
        return new TestId(value);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}