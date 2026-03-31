using System.Security.Claims;

namespace ExtendFile.Panelis.Presentation.Extensions;

public static class HttpContextExtensions
{
    public static string? GetUserId(this HttpContext httpContext)
    {
        var userIdClaim = httpContext.User.FindFirst("Id");
        return userIdClaim?.Value;
    }

    public static string? GetUserEmail(this HttpContext httpContext)
    {
        var emailClaim = httpContext.User.FindFirst(ClaimTypes.Email);
        return emailClaim?.Value;
    }

    public static string? GetUserName(this HttpContext httpContext)
    {
        var nameClaim = httpContext.User.FindFirst(ClaimTypes.Name);
        return nameClaim?.Value;
    }

    public static bool IsAdmin(this HttpContext httpContext)
    {
        var adminClaim = httpContext.User.FindFirst("admin");
        return adminClaim?.Value == true.ToString();
    }

    public static string? GetAdminClaim(this HttpContext httpContext)
    {
        var adminClaim = httpContext.User.FindFirst("admin");
        return adminClaim?.Value;
    }
}
