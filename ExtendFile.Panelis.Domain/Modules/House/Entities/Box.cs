using ExtendFile.Panelis.BuildingBlocks.Common.Class.Base;
using ExtendFile.Panelis.Domain.Exceptions;
using ExtendFile.Panelis.Domain.Modules.Cat.ValueObject;
using ExtendFile.Panelis.Domain.Modules.House.Entities;
using ExtendFile.Panelis.Domain.Modules.House.ValueObject;

namespace ExtendFile.Panelis.Domain.Modules.House.Entities;

public class Box : Entity<BoxId>
{
    public string Name { get; private set; } = string.Empty;
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }
    public int MaxQuantity { get; private set; }

    public Box() { }

    public Box(
        BoxId id,
        string name,
        int maxQuantity) : base(id)
    {
        Name = name;
        MaxQuantity = maxQuantity;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    public static Box Create(string name, int maxQuantity)
    {
        var box = new Box(
            BoxId.CreateIdentifier(),
            name,
            maxQuantity
        );

        return box;
    }

    public void Update(string name, int maxQuantity)
    {
        Name = name;
        MaxQuantity = maxQuantity;
        UpdatedAt = DateTime.UtcNow;
    }
}