using MediatR;
using Microsoft.AspNetCore.Mvc;
using ExtendFile.Panelis.Application.Modules.User.Requests.Login;
using ExtendFile.Panelis.Application.Modules.User.Commands.Login;
using ExtendFile.Panelis.Application.Modules.User.Responses;
using ExtendFile.Panelis.Presentation.Extensions;

namespace ExtendFile.Panelis.Presentation.Controllers.V1;

/// <summary>
/// Controller para operações de autenticação
/// </summary>
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
[ApiController]
public class AuthenticationController : ControllerBase
{
    private readonly IMediator _mediator;
    
    public AuthenticationController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    /// <summary>
    /// Realiza login do usuário
    /// </summary>
    /// <param name="request">Dados de login</param>
    /// <returns>Retorna informações do usuário e token de autenticação</returns>
    [HttpPost("login")]
    [Consumes("application/json")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(UserSessionResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var command = new LoginCommand(request);
        var result = await _mediator.Send(command);
        return result.ToActionResult(this);
    }
}
