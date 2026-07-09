using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.Encodings.Web;
using ExtendFile.Panelis.Application.Interfaces.Clients;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;

namespace ExtendFile.Panelis.Presentation.Middlewares;

public class IntrospectAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{
    public const string SchemeName = "IntrospectScheme";

    private readonly IIdentityServerClient _identityServerClient;

    public IntrospectAuthenticationHandler(
        IOptionsMonitor<AuthenticationSchemeOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder,
        IIdentityServerClient identityServerClient)
        : base(options, logger, encoder)
    {
        _identityServerClient = identityServerClient;
    }

    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        var authorization = Request.Headers.Authorization.ToString();

        if (!authorization.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
            return AuthenticateResult.NoResult();

        var token = authorization["Bearer ".Length..].Trim();

        try
        {
            var result = await _identityServerClient.IntrospectAsync(token, Context.RequestAborted);

            if (!result.Active)
                return AuthenticateResult.Fail("Token inválido ou expirado.");

            var claims = new List<Claim>();

            if (!string.IsNullOrEmpty(result.UserId))
                claims.Add(new Claim("Id", result.UserId));
            if (!string.IsNullOrEmpty(result.Email))
                claims.Add(new Claim(ClaimTypes.Email, result.Email));
            if (!string.IsNullOrEmpty(result.Name))
                claims.Add(new Claim(ClaimTypes.Name, result.Name));

            // Introspect only returns app/module claims; read admin claim directly from
            // the decoded JWT (safe since the token was already validated above).
            var handler = new JwtSecurityTokenHandler();
            if (handler.CanReadToken(token))
            {
                var jwt = handler.ReadJwtToken(token);
                var adminClaim = jwt.Claims.FirstOrDefault(c => c.Type == "admin");
                if (adminClaim is not null)
                    claims.Add(new Claim("admin", adminClaim.Value));
            }

            foreach (var claim in result.Claims)
            {
                var separatorIndex = claim.IndexOf(':');
                if (separatorIndex > 0)
                    claims.Add(new Claim(claim[..separatorIndex], claim[(separatorIndex + 1)..]));
            }

            var identity = new ClaimsIdentity(claims, SchemeName);
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, SchemeName);

            return AuthenticateResult.Success(ticket);
        }
        catch (HttpRequestException ex)
        {
            Logger.LogError(ex, "Identity server indisponível ao validar token");
            Response.StatusCode = StatusCodes.Status503ServiceUnavailable;
            await Response.WriteAsync("Serviço de autenticação indisponível.");
            return AuthenticateResult.Fail("Identity server indisponível.");
        }
    }

    protected override Task HandleChallengeAsync(AuthenticationProperties properties)
    {
        if (!Response.HasStarted)
            Response.StatusCode = StatusCodes.Status401Unauthorized;

        return Task.CompletedTask;
    }

    protected override Task HandleForbiddenAsync(AuthenticationProperties properties)
    {
        if (!Response.HasStarted)
            Response.StatusCode = StatusCodes.Status403Forbidden;

        return Task.CompletedTask;
    }
}
