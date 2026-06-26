using ExtendFile.Panelis.Application.Interfaces.Services;
using ExtendFile.Panelis.Infrastructure.Services;
using ExtendFile.Panelis.Infrastructure.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AuthenticationService = Microsoft.AspNetCore.Authentication.AuthenticationService;
using IAuthenticationService = Microsoft.AspNetCore.Authentication.IAuthenticationService;

namespace ExtendFile.Panelis.CrossCutting.IoC.Infrastructure.Services;

public static class ServicesDependencyInjection
{
    public static void AddServicesDependencyInjection(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IAuthenticationService, AuthenticationService>();
        services.AddScoped<ITokenJwtService, TokenJwtService>();

        services.AddEmailService(configuration);
        services.AddBrevoEmailService(configuration);

        services.AddMemoryCache();
        services.AddSingleton<IPasswordResetCodeStore, PasswordResetCodeStore>();
        services.AddSingleton<ITwoFactorCodeStore, TwoFactorCodeStore>();
    }

    private static void AddEmailService(this IServiceCollection services, IConfiguration configuration)
    {
        var section = configuration.GetSection("EmailSettings");
        var settings = new EmailSettings
        {
            Host = section["Host"] ?? Environment.GetEnvironmentVariable("EMAIL_HOST") ?? string.Empty,
            Port = int.TryParse(section["Port"] ?? Environment.GetEnvironmentVariable("EMAIL_PORT"), out var port) ? port : 587,
            Username = section["Username"] ?? Environment.GetEnvironmentVariable("EMAIL_USERNAME") ?? string.Empty,
            Password = section["Password"] ?? Environment.GetEnvironmentVariable("EMAIL_PASSWORD") ?? string.Empty,
            FromEmail = section["FromEmail"] ?? Environment.GetEnvironmentVariable("EMAIL_FROM_EMAIL") ?? string.Empty,
            FromName = section["FromName"] ?? Environment.GetEnvironmentVariable("EMAIL_FROM_NAME") ?? string.Empty,
        };

        services.Configure<EmailSettings>(options =>
        {
            options.Host = settings.Host;
            options.Port = settings.Port;
            options.Username = settings.Username;
            options.Password = settings.Password;
            options.FromEmail = settings.FromEmail;
            options.FromName = settings.FromName;
        });
        services.AddScoped<IEmailService, EmailService>();
    }

    private static void AddBrevoEmailService(this IServiceCollection services, IConfiguration configuration)
    {
        var section = configuration.GetSection("BrevoSettings");
        var settings = new BrevoSettings
        {
            ApiKey = section["ApiKey"] ?? Environment.GetEnvironmentVariable("BREVO_API_KEY") ?? string.Empty,
            FromEmail = section["FromEmail"] ?? Environment.GetEnvironmentVariable("EMAIL_FROM_EMAIL") ?? string.Empty,
            FromName = section["FromName"] ?? Environment.GetEnvironmentVariable("EMAIL_FROM_NAME") ?? string.Empty,
        };

        services.Configure<BrevoSettings>(options =>
        {
            options.ApiKey = settings.ApiKey;
            options.FromEmail = settings.FromEmail;
            options.FromName = settings.FromName;
        });
        services.AddHttpClient("Brevo", client =>
        {
            client.BaseAddress = new Uri("https://api.brevo.com/v3/");
            client.DefaultRequestHeaders.Add("api-key", settings.ApiKey);
        });
        services.AddScoped<IBrevoEmailService, BrevoEmailService>();
    }
}