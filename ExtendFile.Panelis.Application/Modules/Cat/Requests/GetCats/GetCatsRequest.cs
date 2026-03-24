using System.Text.Json.Serialization;
using ExtendFile.Panelis.BuildingBlocks.Pagination;
using ExtendFile.Panelis.Domain.Modules.Cat.Enums;

namespace ExtendFile.Panelis.Application.Modules.Cat.Requests.GetCats;

public class GetCatsRequest
{
    public string? Name { get; set; }
    public CatLocation? Location { get; set; }
    public bool? IsActive { get; set; }
    public CatSex? Sex { get; set; }
    public PaginationParams PaginationParams { get; set; } = new();
}
