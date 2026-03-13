using ExtendFile.Panelis.BuildingBlocks.Common.Class.AggregateRoot;
using ExtendFile.Panelis.Domain.Modules.Cat.Enums;
using ExtendFile.Panelis.Domain.Modules.Cat.ValueObject;
using ExtendFile.Panelis.Domain.Modules.House.ValueObject;

namespace ExtendFile.Panelis.Domain.Modules.Cat.Aggregates;

public class Cat : AggregateRoot<CatId>
{
    public string Name { get; private set; } = string.Empty;
    public string Hash { get; private set; } = string.Empty;
    public DateTime DateOfBirth { get; private set; }
    public decimal Weight { get; private set; }
    public CatSex Sex { get; private set; }
    public CatLocation Location { get; private set; }
    public BoxId BoxId { get; private set; } = null!;
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }
    public bool IsActive { get; set; }
    public int DaysWithoutEating { get; private set; } = 0;

    public Cat() { }

    public Cat(
        CatId id,
        string name,
        string hash,
        DateTime dateOfBirth,
        decimal weight,
        CatSex sex,
        CatLocation location,
        BoxId boxId,
        int daysWithoutEating = 0) : base(id)
    {
        Name = name;
        Hash = hash;
        DateOfBirth = dateOfBirth;
        Weight = weight;
        Sex = sex;
        Location = location;
        BoxId = boxId;
        DaysWithoutEating = daysWithoutEating;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
        IsActive = true;
    }

    public static Cat Create(
        string name,
        string hash,
        DateTime dateOfBirth,
        decimal weight,
        CatSex sex,
        CatLocation location,
        BoxId boxId)
    {
        var cat = new Cat(
            CatId.CreateIdentifier(),
            name,
            hash,
            dateOfBirth,
            weight,
            sex,
            location,
            boxId
        );

        return cat;
    }

    public void Update(
        string name,
        string hash,
        DateTime dateOfBirth,
        decimal weight,
        CatSex sex,
        CatLocation location,
        bool isActive)
    {
        Name = name;
        Hash = hash;
        DateOfBirth = dateOfBirth;
        Weight = weight;
        Sex = sex;
        Location = location;
        UpdatedAt = DateTime.UtcNow;
        IsActive = isActive;
    }

    public void MoveToBox(BoxId boxId)
    {
        BoxId = boxId;
        UpdatedAt = DateTime.UtcNow;
    }

    public void IncrementDaysWithoutEating()
    {
        DaysWithoutEating++;
        UpdatedAt = DateTime.UtcNow;
    }

    public void ResetDaysWithoutEating()
    {
        DaysWithoutEating = 0;
        UpdatedAt = DateTime.UtcNow;
    }

    public void SetDaysWithoutEating(int days)
    {
        DaysWithoutEating = days >= 0 ? days : 0;
        UpdatedAt = DateTime.UtcNow;
    }
}