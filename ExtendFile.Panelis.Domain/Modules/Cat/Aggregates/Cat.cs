using ExtendFile.Panelis.BuildingBlocks.Common.Class.AggregateRoot;
using ExtendFile.Panelis.Domain.Modules.Cat.Enums;
using ExtendFile.Panelis.Domain.Modules.Cat.ValueObject;
using ExtendFile.Panelis.Domain.Modules.House.ValueObject;

namespace ExtendFile.Panelis.Domain.Modules.Cat.Aggregates;

public class Cat : AggregateRoot<CatId>
{
    public string Name { get; private set; } = string.Empty;
    public string Hash { get; private set; } = string.Empty;
    public int Age { get; private set; }
    public decimal Weight { get; private set; }
    public CatSex Sex { get; private set; }
    public BoxId BoxId { get; private set; } = null!;
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }
    public bool IsActive { get; set; }

    public Cat() { }

    public Cat(
        CatId id,
        string name,
        string hash,
        int age,
        decimal weight,
        CatSex sex,
        BoxId boxId) : base(id)
    {
        Name = name;
        Hash = hash;
        Age = age;
        Weight = weight;
        Sex = sex;
        BoxId = boxId;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
        IsActive = true;
    }

    public static Cat Create(
        string name,
        string hash,
        int age,
        decimal weight,
        CatSex sex,
        BoxId boxId)
    {
        var cat = new Cat(
            CatId.CreateIdentifier(),
            name,
            hash,
            age,
            weight,
            sex,
            boxId
        );

        return cat;
    }

    public void Update(
        string name,
        string hash,
        int age,
        decimal weight,
        CatSex sex,
        bool isActive)
    {
        Name = name;
        Hash = hash;
        Age = age;
        Weight = weight;
        Sex = sex;
        UpdatedAt = DateTime.UtcNow;
        IsActive = isActive;
    }

    public void MoveToBox(BoxId boxId)
    {
        BoxId = boxId;
        UpdatedAt = DateTime.UtcNow;
    }
}