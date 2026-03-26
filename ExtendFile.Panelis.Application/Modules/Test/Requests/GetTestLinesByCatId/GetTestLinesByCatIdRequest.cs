using ExtendFile.Panelis.BuildingBlocks.Pagination;

namespace ExtendFile.Panelis.Application.Modules.Test.Requests.GetTestLinesByCatId;

public class GetTestLinesByCatIdRequest
{
    public Guid CatId { get; set; }
    public PaginationParams PaginationParams { get; set; } = new();
}
