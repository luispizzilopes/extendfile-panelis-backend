using ExtendFile.Panelis.BuildingBlocks.Pagination;

namespace ExtendFile.Panelis.Application.Modules.Test.Requests.GetTestsByBoxId;

public class GetTestsByBoxIdRequest
{
    public PaginationParams PaginationParams { get; set; } = new();
    public Guid BoxId { get; set; }
}
