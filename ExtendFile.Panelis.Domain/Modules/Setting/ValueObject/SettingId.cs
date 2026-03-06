namespace ExtendFile.Panelis.Domain.Modules.Setting.ValueObject;

public class SettingId : BuildingBlocks.Common.Class.ValueObject.ValueObject
{
    public Guid Value { get; private set; }

    private SettingId(Guid value)
    {
        Value = value;
    }

    public static SettingId CreateIdentifier()
    {
        return new(Guid.NewGuid());
    }

    public static SettingId Create(Guid value)
    {
        return new SettingId(value);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
