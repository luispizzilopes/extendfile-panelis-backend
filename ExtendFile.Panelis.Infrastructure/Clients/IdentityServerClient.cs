using System.Net.Http.Json;
using ExtendFile.Panelis.Application.Interfaces.Clients;

namespace ExtendFile.Panelis.Infrastructure.Clients;

public class IdentityServerClient : IIdentityServerClient
{
    private readonly HttpClient _httpClient;

    public IdentityServerClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IntrospectResult> IntrospectAsync(string token, CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.PostAsJsonAsync(
            "api/v1/Introspect",
            new { Token = token },
            cancellationToken);

        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<IntrospectResponse>(cancellationToken: cancellationToken)
            ?? throw new InvalidOperationException("Resposta inválida do identity server.");

        return new IntrospectResult(result.Active, result.UserId, result.Email, result.Name, result.Claims ?? []);
    }

    private sealed record IntrospectResponse(bool Active, string? UserId, string? Email, string? Name, IEnumerable<string>? Claims);
}
