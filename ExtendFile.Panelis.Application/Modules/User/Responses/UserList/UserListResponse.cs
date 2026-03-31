namespace ExtendFile.Panelis.Application.Modules.User.Responses;

public class UserListResponse
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? Picture { get; set; }
    public bool? Active { get; set; }
}
