using ExtendFile.Panelis.Domain.Modules.Cat.Enums;

namespace ExtendFile.Panelis.Application.Modules.Cat.Responses;

public class CatDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Hash { get; set; } = string.Empty;
    public int Age { get; set; }
    public decimal Weight { get; set; }
    public CatSex Sex { get; set; }
    public Guid BoxId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public bool IsActive { get; set; }
    public string HouseName  { get; set; } = string.Empty;
    public string BoxName  { get; set; } = string.Empty;
}
