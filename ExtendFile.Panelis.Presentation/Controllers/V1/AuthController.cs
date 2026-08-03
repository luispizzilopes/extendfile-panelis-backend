using ExtendFile.Panelis.Application.Interfaces.Clients;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExtendFile.Panelis.Presentation.Controllers.V1;

[AllowAnonymous]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IIdentityServerClient _identityServerClient;

    public AuthController(IIdentityServerClient identityServerClient)
    {
        _identityServerClient = identityServerClient;
    }

    [HttpPost("validate-token")]
    [ProducesResponseType(typeof(ValidateTokenResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
    public async Task<IActionResult> ValidateToken([FromBody] ValidateTokenRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _identityServerClient.IntrospectAsync(request.Token, cancellationToken);
            return Ok(new ValidateTokenResponse(result.Active, result.UserId, result.Email, result.Name, result.Claims));
        }
        catch (HttpRequestException)
        {
            return StatusCode(StatusCodes.Status503ServiceUnavailable);
        }
    }
}

public record ValidateTokenRequest(string Token);
public record ValidateTokenResponse(bool Active, string? UserId, string? Email, string? Name, IEnumerable<string> Claims);
