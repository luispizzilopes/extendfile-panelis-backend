using ExtendFile.Panelis.BuildingBlocks.Common.Class.AggregateRoot;
using ExtendFile.Panelis.Domain.Modules.Setting.ValueObject;

namespace ExtendFile.Panelis.Domain.Modules.Setting.Aggregates;

public class Setting : AggregateRoot<SettingId>
{
    public decimal LessThanEnoughThreshold { get; private set; }
    public decimal MoreThanEnoughThreshold { get; private set; }
    public int DaysWithoutEatingForAlert { get; private set; }
    public int DaysWithoutEatingForWarning { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }

    private Setting(
        SettingId id,
        decimal lessThanEnoughThreshold,
        decimal moreThanEnoughThreshold,
        int daysWithoutEatingForAlert,
        int daysWithoutEatingForWarning) : base(id)
    {
        LessThanEnoughThreshold = lessThanEnoughThreshold;
        MoreThanEnoughThreshold = moreThanEnoughThreshold;
        DaysWithoutEatingForAlert = daysWithoutEatingForAlert;
        DaysWithoutEatingForWarning = daysWithoutEatingForWarning;
        CreatedAt = DateTime.UtcNow;
    }

    public static Setting Create(
        decimal lessThanEnoughThreshold,
        decimal moreThanEnoughThreshold,
        int daysWithoutEatingForAlert = 3,
        int daysWithoutEatingForWarning = 5)
    {
        var id = SettingId.CreateIdentifier();

        return new Setting(
            id,
            lessThanEnoughThreshold,
            moreThanEnoughThreshold,
            daysWithoutEatingForAlert,
            daysWithoutEatingForWarning);
    }

    public void Update(
        decimal lessThanEnoughThreshold,
        decimal moreThanEnoughThreshold,
        int daysWithoutEatingForAlert,
        int daysWithoutEatingForWarning)
    {
        LessThanEnoughThreshold = lessThanEnoughThreshold;
        MoreThanEnoughThreshold = moreThanEnoughThreshold;
        DaysWithoutEatingForAlert = daysWithoutEatingForAlert;
        DaysWithoutEatingForWarning = daysWithoutEatingForWarning;
        UpdatedAt = DateTime.UtcNow;
    }
}
