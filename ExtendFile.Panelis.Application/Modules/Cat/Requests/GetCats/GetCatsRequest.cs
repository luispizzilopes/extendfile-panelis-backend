using ExtendFile.Panelis.BuildingBlocks.Pagination;

namespace ExtendFile.Panelis.Application.Modules.Cat.Requests.GetCats;

public class GetCatsRequest
{
    public string? Search { get; set; }
    public PaginationParams PaginationParams { get; set; } = new();
}
