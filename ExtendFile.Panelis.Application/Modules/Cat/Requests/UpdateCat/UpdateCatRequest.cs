using ExtendFile.Panelis.Domain.Modules.Cat.Enums;

namespace ExtendFile.Panelis.Application.Modules.Cat.Requests.UpdateCat;

public class UpdateCatRequest
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Hash { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; }
    public decimal Weight { get; set; }
    public CatSex Sex { get; set; }
    public CatLocation Location { get; set; }
    public Guid BoxId { get; set; }
    public bool IsActive { get; set; }
}
