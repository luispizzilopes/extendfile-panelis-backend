using ExtendFile.Panelis.BuildingBlocks.Pagination;

namespace ExtendFile.Panelis.Application.Modules.Cat.Requests.GetCatsByBoxId;

public class GetCatsByBoxIdRequest
{
    public PaginationParams PaginationParams { get; set; } = new();
    public Guid BoxId { get; set; }
}
