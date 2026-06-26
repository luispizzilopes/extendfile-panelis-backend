using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using ExtendFile.Panelis.Application.Modules.User.Requests.Login;
using ExtendFile.Panelis.Application.Modules.User.Commands.Login;
using ExtendFile.Panelis.Application.Modules.User.Responses;
using ExtendFile.Panelis.Application.Modules.User.Requests.RequestPasswordReset;
using ExtendFile.Panelis.Application.Modules.User.Commands.RequestPasswordReset;
using ExtendFile.Panelis.Application.Modules.User.Responses.RequestPasswordReset;
using ExtendFile.Panelis.Application.Modules.User.Requests.VerifyPasswordResetCode;
using ExtendFile.Panelis.Application.Modules.User.Commands.VerifyPasswordResetCode;
using ExtendFile.Panelis.Application.Modules.User.Responses.VerifyPasswordResetCode;
using ExtendFile.Panelis.Application.Modules.User.Requests.ConfirmPasswordReset;
using ExtendFile.Panelis.Application.Modules.User.Commands.ConfirmPasswordReset;
using ExtendFile.Panelis.Application.Modules.User.Responses.ConfirmPasswordReset;
using ExtendFile.Panelis.Application.Modules.User.Requests.TwoFactor;
using ExtendFile.Panelis.Application.Modules.User.Commands.VerifyTwoFactor;
using ExtendFile.Panelis.Application.Modules.User.Commands.EnableTwoFactor;
using ExtendFile.Panelis.Application.Modules.User.Commands.DisableTwoFactor;
using ExtendFile.Panelis.Application.Modules.User.Responses.TwoFactor;
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
    /// <returns>Retorna informações do usuário e token de autenticação, ou indica que 2FA é necessário</returns>
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

    /// <summary>
    /// Verifica o código de dois fatores e retorna a sessão do usuário
    /// </summary>
    /// <param name="request">E-mail e código de 6 dígitos</param>
    /// <returns>Retorna informações do usuário e token de autenticação</returns>
    [HttpPost("verify-two-factor")]
    [Consumes("application/json")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(UserSessionResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> VerifyTwoFactor([FromBody] VerifyTwoFactorRequest request)
    {
        var command = new VerifyTwoFactorCommand(request);
        var result = await _mediator.Send(command);
        return result.ToActionResult(this);
    }

    /// <summary>
    /// Habilita autenticação de dois fatores para o usuário autenticado
    /// </summary>
    /// <returns>Confirmação da operação</returns>
    [HttpPost("enable-two-factor")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Produces("application/json")]
    [ProducesResponseType(typeof(EnableTwoFactorResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> EnableTwoFactor()
    {
        var email = HttpContext.GetUserEmail();
        var command = new EnableTwoFactorCommand(email!);
        var result = await _mediator.Send(command);
        return result.ToActionResult(this);
    }

    /// <summary>
    /// Desabilita autenticação de dois fatores para o usuário autenticado
    /// </summary>
    /// <returns>Confirmação da operação</returns>
    [HttpPost("disable-two-factor")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Produces("application/json")]
    [ProducesResponseType(typeof(DisableTwoFactorResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DisableTwoFactor()
    {
        var email = HttpContext.GetUserEmail();
        var command = new DisableTwoFactorCommand(email!);
        var result = await _mediator.Send(command);
        return result.ToActionResult(this);
    }

    /// <summary>
    /// Solicita código de recuperação de senha por e-mail
    /// </summary>
    /// <param name="request">E-mail do usuário</param>
    /// <returns>Confirmação genérica (não revela se o e-mail existe)</returns>
    [HttpPost("forgot-password")]
    [Consumes("application/json")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(RequestPasswordResetResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> ForgotPassword([FromBody] RequestPasswordResetRequest request)
    {
        var command = new RequestPasswordResetCommand(request);
        var result = await _mediator.Send(command);
        return result.ToActionResult(this);
    }

    /// <summary>
    /// Verifica o código de recuperação de senha informado pelo usuário
    /// </summary>
    /// <param name="request">E-mail e código de verificação</param>
    /// <returns>Confirmação de validade do código</returns>
    [HttpPost("verify-reset-code")]
    [Consumes("application/json")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(VerifyPasswordResetCodeResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> VerifyResetCode([FromBody] VerifyPasswordResetCodeRequest request)
    {
        var command = new VerifyPasswordResetCodeCommand(request);
        var result = await _mediator.Send(command);
        return result.ToActionResult(this);
    }

    /// <summary>
    /// Confirma a nova senha após validação do código
    /// </summary>
    /// <param name="request">E-mail, código e nova senha</param>
    /// <returns>Confirmação de redefinição de senha</returns>
    [HttpPost("confirm-password-reset")]
    [Consumes("application/json")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(ConfirmPasswordResetResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> ConfirmPasswordReset([FromBody] ConfirmPasswordResetRequest request)
    {
        var command = new ConfirmPasswordResetCommand(request);
        var result = await _mediator.Send(command);
        return result.ToActionResult(this);
    }
}
