using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ExtendFile.Panelis.Application.Modules.User.Requests;
using ExtendFile.Panelis.Application.Modules.User.Queries.UserList;
using ExtendFile.Panelis.BuildingBlocks.Pagination;
using ExtendFile.Panelis.Application.Modules.User.Responses;
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
[Authorize(Policy = "RequireAdminClaim")]
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
}
