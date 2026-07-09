using ExtendFile.Panelis.Application.Interfaces.Clients;
using ExtendFile.Panelis.Infrastructure.Clients;
using ExtendFile.Panelis.Presentation.Middlewares;
using Microsoft.AspNetCore.Authentication;

namespace ExtendFile.Panelis.Presentation.Extensions;

public static class IntrospectAuthExtension
{
    public static void AddIntrospectAuth(this IServiceCollection services, IConfiguration configuration)
    {
        var baseUrl = Resolve(configuration, "IdentityServer:BaseUrl", "IDENTITY_SERVER_BASE_URL");
        var apiKey  = Resolve(configuration, "IdentityServer:ApiKey",  "IDENTITY_SERVER_API_KEY");

        services.AddHttpClient<IIdentityServerClient, IdentityServerClient>(client =>
        {
            client.BaseAddress = new Uri(baseUrl);
            client.DefaultRequestHeaders.Add("X-Api-Key", apiKey);
        });

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = IntrospectAuthenticationHandler.SchemeName;
            options.DefaultChallengeScheme    = IntrospectAuthenticationHandler.SchemeName;
            options.DefaultForbidScheme       = IntrospectAuthenticationHandler.SchemeName;
        })
        .AddScheme<AuthenticationSchemeOptions, IntrospectAuthenticationHandler>(
            IntrospectAuthenticationHandler.SchemeName, null);

        services.AddAuthorization(options =>
            options.AddPolicy("RequireAdminClaim", policy =>
                policy.RequireClaim("admin", "true")));
    }

    private static string Resolve(IConfiguration configuration, string configKey, string envVar)
    {
        var value = configuration[configKey];
        if (string.IsNullOrEmpty(value))
            value = Environment.GetEnvironmentVariable(envVar);
        if (string.IsNullOrEmpty(value))
            throw new InvalidOperationException($"Configuração '{configKey}' é obrigatória.");
        return value;
    }
}
