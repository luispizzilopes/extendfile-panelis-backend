using System.Net.Http.Json;
using ExtendFile.Panelis.Application.Interfaces.Services;
using ExtendFile.Panelis.Infrastructure.Settings;
using Microsoft.Extensions.Options;

namespace ExtendFile.Panelis.Infrastructure.Services;

public class BrevoEmailService : IBrevoEmailService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly BrevoSettings _settings;

    public BrevoEmailService(IHttpClientFactory httpClientFactory, IOptions<BrevoSettings> settings)
    {
        _httpClientFactory = httpClientFactory;
        _settings = settings.Value;
    }

    public async Task SendAsync(string to, string subject, string body, CancellationToken cancellationToken = default)
    {
        var payload = new
        {
            sender = new { name = _settings.FromName, email = _settings.FromEmail },
            to = new[] { new { email = to } },
            subject,
            htmlContent = body,
        };

        var client = _httpClientFactory.CreateClient("Brevo");
        await client.PostAsJsonAsync("smtp/email", payload, cancellationToken);
    }
}
