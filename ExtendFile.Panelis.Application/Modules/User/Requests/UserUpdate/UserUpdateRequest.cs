namespace ExtendFile.Panelis.Application.Modules.User.Requests;

public class UserUpdateRequest
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? Password { get; set; }
    public bool Active { get; set; }
}
