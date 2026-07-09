using MediatR;
using Microsoft.AspNetCore.Mvc;
using ExtendFile.Panelis.Application.Modules.Cat.Requests.CreateCat;
using ExtendFile.Panelis.Application.Modules.Cat.Requests.UpdateCat;
using ExtendFile.Panelis.Application.Modules.Cat.Requests.DeleteCat;
using ExtendFile.Panelis.Application.Modules.Cat.Requests.GetCatById;
using ExtendFile.Panelis.Application.Modules.Cat.Requests.GetCats;
using ExtendFile.Panelis.Application.Modules.Cat.Requests.GetCatsByBoxId;
using ExtendFile.Panelis.Application.Modules.Cat.Queries.GetCatById;
using ExtendFile.Panelis.Application.Modules.Cat.Queries.GetCats;
using ExtendFile.Panelis.Application.Modules.Cat.Queries.GetCatsByBoxId;
using ExtendFile.Panelis.Application.Modules.Cat.Commands.CreateCat;
using ExtendFile.Panelis.Application.Modules.Cat.Commands.UpdateCat;
using ExtendFile.Panelis.Application.Modules.Cat.Commands.DeleteCat;
using ExtendFile.Panelis.Application.Modules.Cat.Responses;
using ExtendFile.Panelis.Application.Modules.Cat.Responses.DeleteCat;
using ExtendFile.Panelis.BuildingBlocks.Pagination;
using ExtendFile.Panelis.Domain.Modules.Cat.Enums;
using ExtendFile.Panelis.Presentation.Extensions;
using Microsoft.AspNetCore.Authorization;

namespace ExtendFile.Panelis.Presentation.Controllers.V1;

/// <summary>
/// Controller para operações de Gatos
/// </summary>
[Authorize]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
[ApiController]
public class CatController : ControllerBase
{
    private readonly IMediator _mediator;
    
    public CatController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    /// <summary>
    /// Obtém gato por ID
    /// </summary>
    /// <param name="id">ID do gato</param>
    /// <returns>Retorna os dados do gato</returns>
    [HttpGet("{id}")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(CatDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetById(Guid id)
    {
        var request = new GetCatByIdRequest { Id = id };
        var query = new GetCatByIdQuery(request);
        var result = await _mediator.Send(query);
        return result.ToActionResult(this);
    }
    
    /// <summary>
    /// Obtém lista paginada de gatos
    /// </summary>
    /// <param name="name">Nome do gato</param>
    /// <param name="location">Localização do gato</param>
    /// <param name="isActive">Se o gato está ativo</param>
    /// <param name="sex">Sexo do gato</param>
    /// <param name="paginationParams">Parâmetros de paginação</param>
    /// <returns>Retorna lista paginada de gatos</returns>
    [HttpGet]
    [Produces("application/json")]
    [ProducesResponseType(typeof(PaginedResult<CatDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetCats(
        [FromQuery] string? name,
        [FromQuery] CatLocation? location,
        [FromQuery] bool? isActive,
        [FromQuery] CatSex? sex,
        [FromQuery] PaginationParams paginationParams)
    {
        var request = new GetCatsRequest
        {
            Name = name,
            Location = location,
            IsActive = isActive,
            Sex = sex,
            PaginationParams = paginationParams
        };

        var query = new GetCatsQuery(request);
        var result = await _mediator.Send(query);
        return result.ToActionResult(this);
    }
    
    /// <summary>
    /// Obtém lista paginada de gatos por Box ID
    /// </summary>
    /// <param name="boxId">ID do box</param>
    /// <param name="paginationParams">Parâmetros de paginação</param>
    /// <returns>Retorna lista paginada de gatos do box especificado</returns>
    [HttpGet("by-box/{boxId}")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(PaginedResult<CatDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetCatsByBoxId(
        Guid boxId,
        [FromQuery] PaginationParams paginationParams)
    {
        var request = new GetCatsByBoxIdRequest 
        { 
            BoxId = boxId,
            PaginationParams = paginationParams
        };
        var query = new GetCatsByBoxIdQuery(request);
        var result = await _mediator.Send(query);
        return result.ToActionResult(this);
    }
    
    /// <summary>
    /// Cria um novo gato
    /// </summary>
    /// <param name="request">Dados do gato a ser criado</param>
    /// <returns>Retorna os dados do gato criado</returns>
    [HttpPost]
    [Consumes("application/json")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(CatDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateCat([FromBody] CreateCatRequest request)
    {
        var command = new CreateCatCommand(request);
        var result = await _mediator.Send(command);
        return result.ToActionResult(this);
    }
    
    /// <summary>
    /// Atualiza um gato existente
    /// </summary>
    /// <param name="request">Dados do gato a ser atualizado</param>
    /// <returns>Retorna os dados do gato atualizado</returns>
    [HttpPut]
    [Consumes("application/json")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(CatDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateCat([FromBody] UpdateCatRequest request)
    {
        var command = new UpdateCatCommand(request);
        var result = await _mediator.Send(command);
        return result.ToActionResult(this);
    }
    
    /// <summary>
    /// Exclui um gato existente
    /// </summary>
    /// <param name="id">ID do gato</param>
    /// <returns>Retorna sucesso da operação</returns>
    [HttpDelete("{id}")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(DeleteCatResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteCat(Guid id)
    {
        var request = new DeleteCatRequest { Id = id };
        var command = new DeleteCatCommand(request);
        var result = await _mediator.Send(command);
        return result.ToActionResult(this);
    }
}
