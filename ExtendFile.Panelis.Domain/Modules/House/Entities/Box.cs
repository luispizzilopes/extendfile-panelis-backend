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
    private int MaxQuantity => 10;
    
    private readonly List<CatId> _catIds = [];
    public IReadOnlyList<CatId> CatIds => _catIds.AsReadOnly();

    public Box() { }

    public Box(
        BoxId id,
        string name) : base(id)
    {
        Name = name;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    public static Box Create(string name)
    {
        var box = new Box(
            BoxId.CreateIdentifier(),
            name
        );

        return box;
    }

    public void Update(string name)
    {
        Name = name;
        UpdatedAt = DateTime.UtcNow;
    }
    
    
    public void AddCat(CatId catId)
    {
        if (_catIds.Count >= MaxQuantity)
            throw new DomainException($"Box already reached the maximum capacity of {MaxQuantity} cats.");

        if (_catIds.Contains(catId))
            throw new DomainException("Cat is already in this box.");

        _catIds.Add(catId);
    }

    public void RemoveCat(CatId catId)
    {
        if (!_catIds.Contains(catId))
            throw new DomainException("Cat not found in this box.");

        _catIds.Remove(catId);
    }
}