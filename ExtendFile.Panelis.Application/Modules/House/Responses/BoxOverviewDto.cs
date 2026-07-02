using ExtendFile.Panelis.Application.Modules.Cat.Responses;

namespace ExtendFile.Panelis.Application.Modules.House.Responses;

public class BoxOverviewDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public Guid HouseId { get; set; }
    public int? MaxQuantity { get; set; }
    public int? Quantity { get; set; }
    public int? DaysWithoutTest { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public IEnumerable<CatDto> Cats { get; set; } = [];
}
