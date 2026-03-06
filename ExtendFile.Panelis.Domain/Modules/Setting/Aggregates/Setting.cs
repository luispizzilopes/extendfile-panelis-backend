using ExtendFile.Panelis.BuildingBlocks.Common.Class.AggregateRoot;
using ExtendFile.Panelis.Domain.Modules.Setting.ValueObject;

namespace ExtendFile.Panelis.Domain.Modules.Setting.Aggregates;

public class Setting : AggregateRoot<SettingId>
{
    public decimal LessThanEnoughThreshold { get; private set; }
    public decimal MoreThanEnoughThreshold { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }

    private Setting(
        SettingId id,
        decimal lessThanEnoughThreshold,
        decimal moreThanEnoughThreshold) : base(id)
    {
        LessThanEnoughThreshold = lessThanEnoughThreshold;
        MoreThanEnoughThreshold = moreThanEnoughThreshold;
        CreatedAt = DateTime.UtcNow;
    }

    public static Setting Create(
        decimal lessThanEnoughThreshold,
        decimal moreThanEnoughThreshold)
    {
        var id = SettingId.CreateIdentifier();

        return new Setting(
            id,
            lessThanEnoughThreshold,
            moreThanEnoughThreshold);
    }

    public void Update(
        decimal lessThanEnoughThreshold,
        decimal moreThanEnoughThreshold)
    {
        LessThanEnoughThreshold = lessThanEnoughThreshold;
        MoreThanEnoughThreshold = moreThanEnoughThreshold;
        UpdatedAt = DateTime.UtcNow;
    }
}
