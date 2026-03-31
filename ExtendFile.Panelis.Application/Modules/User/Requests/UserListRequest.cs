using ExtendFile.Panelis.BuildingBlocks.Pagination;

namespace ExtendFile.Panelis.Application.Modules.User.Requests;

public class UserListRequest
{
    public string? Name { get; set; }
    public string? Email { get; set; }
    public PaginationParams PaginationParams { get; set; } = null!;
}
