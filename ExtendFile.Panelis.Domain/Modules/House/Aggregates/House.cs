using ExtendFile.Panelis.BuildingBlocks.Common.Class.AggregateRoot;
using ExtendFile.Panelis.Domain.Modules.House.Entities;
using ExtendFile.Panelis.Domain.Modules.House.ValueObject;

namespace ExtendFile.Panelis.Domain.Modules.House.Aggregates;

public class House : AggregateRoot<HouseId>
{
    public string Name { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }
    
    private readonly List<Box> _boxes = [];
    public List<Box> Boxes => _boxes;
    
    public House(){ }

    public House(
        HouseId id,
        string name,
        string description) : base(id)
    {
        Name = name;
        Description = description;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    public static House Create(string name, string description)
    {
        var house = new House(
            HouseId.CreateIdentifier(),
            name,
            description
        );
        
        return house;
    }

    public void Update(string name, string description)
    {
        Name = name;
        Description = description;
        UpdatedAt = DateTime.UtcNow;
    }

    public void AddBox(Box box)
    {
        _boxes.Add(box);
    }

    public void RemoveBox(Box box)
    {
        _boxes.Remove(box);
    }
}