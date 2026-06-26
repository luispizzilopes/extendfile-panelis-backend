using Microsoft.AspNetCore.Identity;

namespace ExtendFile.Panelis.Domain.Modules.User.Entities;

public class User : IdentityUser
{
    public string Name { get; set; } = string.Empty;
    public string? Picture { get; set; }
    public bool? Active { get; set; }
    public DateTime? LastLoginAt { get; set; }
}