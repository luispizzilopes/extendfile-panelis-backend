using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ExtendFile.Panelis.Application.Modules.User.Requests;
using ExtendFile.Panelis.Application.Modules.User.Queries.UserList;
using ExtendFile.Panelis.Application.Modules.User.Commands;
using ExtendFile.Panelis.Application.Modules.User.Commands.ResetPassword;
using ExtendFile.Panelis.BuildingBlocks.Pagination;
using ExtendFile.Panelis.Application.Modules.User.Responses;
using ExtendFile.Panelis.Application.Modules.User.Requests.ResetPassword;
using ExtendFile.Panelis.Application.Modules.User.Responses.ResetPassword;
using ExtendFile.Panelis.Presentation.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace ExtendFile.Panelis.Presentation.Controllers.V1;

/// <summary>
/// Controller para operações de usuários
/// </summary>
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;
    
    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    /// <summary>
    /// Lista usuários com paginação e filtros
    /// </summary>
    /// <param name="name">Filtro por nome</param>
    /// <param name="email">Filtro por email</param>
    /// <param name="paginationParams">Parâmetros de paginação</param>
    /// <returns>Retorna lista paginada de usuários</returns>
    [HttpGet]
    [Authorize(Policy = "RequireAdminClaim")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(PaginedResult<UserListResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> List(
        [FromQuery] PaginationParams paginationParams,
        [FromQuery] string? name = null,
        [FromQuery] string? email = null)
    {
        var request = new UserListRequest
        {
            Name = name,
            Email = email,
            PaginationParams = paginationParams
        };

        var query = new UserListQuery(request);
        var result = await _mediator.Send(query);
        return result.ToActionResult(this);
    }
    
    /// <summary>
    /// Atualiza dados de um usuário
    /// </summary>
    /// <param name="request">Dados para atualização</param>
    /// <returns>Retorna usuário atualizado</returns>
    [HttpPut]
    [Authorize(Policy = "RequireAdminClaim")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(UserUpdateResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Update([FromBody] UserUpdateRequest request)
    {
        var command = new UserUpdateCommand(request);
        var result = await _mediator.Send(command);
        return result.ToActionResult(this);
    }
    
    /// <summary>
    /// Cria um novo usuário
    /// </summary>
    /// <param name="request">Dados para criação do usuário</param>
    /// <returns>Retorna usuário criado com senha gerada</returns>
    [HttpPost]
    [Authorize(Policy = "RequireAdminClaim")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(UserCreateResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Create([FromBody] UserCreateRequest request)
    {
        var command = new UserCreateCommand(request);
        var result = await _mediator.Send(command);
        return result.ToActionResult(this);
    }
    
    /// <summary>
    /// Altera a senha do usuário logado
    /// </summary>
    /// <param name="request">Dados para alteração de senha</param>
    /// <returns>Retorna resultado da alteração de senha</returns>
    [HttpPut("change-password")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(ResetPasswordResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> ChangePassword([FromBody] ResetPasswordRequest request)
    {
        var userEmail = HttpContext.GetUserEmail();
        if (string.IsNullOrEmpty(userEmail)) return Unauthorized("Usuário não autenticado.");
        
        var command = new ResetPasswordCommand(request, userEmail);
        var result = await _mediator.Send(command);
        return result.ToActionResult(this);
    }
}
