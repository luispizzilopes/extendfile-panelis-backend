namespace ExtendFile.Panelis.Application.Interfaces.Clients;

public record IntrospectResult(bool Active, string? UserId, string? Email, string? Name, IEnumerable<string> Claims);

public interface IIdentityServerClient
{
    Task<IntrospectResult> IntrospectAsync(string token, CancellationToken cancellationToken = default);
}
