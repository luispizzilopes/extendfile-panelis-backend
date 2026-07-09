using MediatR;
using Microsoft.AspNetCore.Mvc;
using ExtendFile.Panelis.Application.Modules.House.Requests.CreateHouse;
using ExtendFile.Panelis.Application.Modules.House.Requests.UpdateHouse;
using ExtendFile.Panelis.Application.Modules.House.Requests.DeleteHouse;
using ExtendFile.Panelis.Application.Modules.House.Requests.CreateBox;
using ExtendFile.Panelis.Application.Modules.House.Requests.UpdateBox;
using ExtendFile.Panelis.Application.Modules.House.Requests.DeleteBox;
using ExtendFile.Panelis.Application.Modules.House.Requests.GetHouseById;
using ExtendFile.Panelis.Application.Modules.House.Requests.GetHouses;
using ExtendFile.Panelis.Application.Modules.House.Requests.GetHousesOverview;
using ExtendFile.Panelis.Application.Modules.House.Queries.GetHouseById;
using ExtendFile.Panelis.Application.Modules.House.Queries.GetHouses;
using ExtendFile.Panelis.Application.Modules.House.Queries.GetHousesOverview;
using ExtendFile.Panelis.Application.Modules.House.Commands.CreateHouse;
using ExtendFile.Panelis.Application.Modules.House.Commands.UpdateHouse;
using ExtendFile.Panelis.Application.Modules.House.Commands.DeleteHouse;
using ExtendFile.Panelis.Application.Modules.House.Commands.CreateBox;
using ExtendFile.Panelis.Application.Modules.House.Commands.UpdateBox;
using ExtendFile.Panelis.Application.Modules.House.Commands.DeleteBox;
using ExtendFile.Panelis.Application.Modules.House.Responses;
using ExtendFile.Panelis.Application.Modules.House.Responses.DeleteHouse;
using ExtendFile.Panelis.Application.Modules.House.Responses.DeleteBox;
using ExtendFile.Panelis.BuildingBlocks.Pagination;
using ExtendFile.Panelis.Presentation.Extensions;
using Microsoft.AspNetCore.Authorization;

namespace ExtendFile.Panelis.Presentation.Controllers.V1;

/// <summary>
/// Controller para operações de Casas/Prédios
/// </summary>
[Authorize]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
[ApiController]
public class HouseController : ControllerBase
{
    private readonly IMediator _mediator;
    
    public HouseController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    /// <summary>
    /// Obtém visão geral hierárquica de prédios, boxes e gatos
    /// </summary>
    /// <returns>Retorna lista de prédios com seus boxes e gatos</returns>
    [HttpGet("overview")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(IEnumerable<HouseOverviewDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetOverview()
    {
        var request = new GetHousesOverviewRequest();
        var query = new GetHousesOverviewQuery(request);
        var result = await _mediator.Send(query);
        return result.ToActionResult(this);
    }

    /// <summary>
    /// Obtém casa/prédio por ID
    /// </summary>
    /// <param name="id">ID da casa/prédio</param>
    /// <returns>Retorna os dados da casa/prédio</returns>
    [HttpGet("{id}")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(HouseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetById(Guid id)
    {
        var request = new GetHouseByIdRequest { Id = id };
        var query = new GetHouseByIdQuery(request);
        var result = await _mediator.Send(query);
        return result.ToActionResult(this);
    }
    
    /// <summary>
    /// Obtém lista paginada de casas/prédios
    /// </summary>
    /// <param name="paginationParams">Parâmetros de paginação</param>
    /// <returns>Retorna lista paginada de casas/prédios</returns>
    [HttpGet]
    [Produces("application/json")]
    [ProducesResponseType(typeof(PaginedResult<HouseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetHouses(
        [FromQuery] PaginationParams paginationParams)
    {
        var request = new GetHousesRequest(paginationParams);
        var query = new GetHousesQuery(request);
        var result = await _mediator.Send(query);
        return result.ToActionResult(this);
    }
    
    /// <summary>
    /// Cria uma nova casa/prédio
    /// </summary>
    /// <param name="request">Dados da casa/prédio a ser criada</param>
    /// <returns>Retorna os dados da casa/prédio criada</returns>
    [HttpPost]
    [Consumes("application/json")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(HouseDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateHouse([FromBody] CreateHouseRequest request)
    {
        var command = new CreateHouseCommand(request);
        var result = await _mediator.Send(command);
        return result.ToActionResult(this);
    }
    
    /// <summary>
    /// Cria um novo box em uma casa/prédio
    /// </summary>
    /// <param name="request">Dados do box a ser criado</param>
    /// <returns>Retorna os dados do box criado</returns>
    [HttpPost("box")]
    [Consumes("application/json")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(BoxDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateBox([FromBody] CreateBoxRequest request)
    {
        var command = new CreateBoxCommand(request);
        var result = await _mediator.Send(command);
        return result.ToActionResult(this);
    }
    
    /// <summary>
    /// Atualiza um box existente
    /// </summary>
    /// <param name="request">Dados do box a ser atualizado</param>
    /// <returns>Retorna os dados do box atualizado</returns>
    [HttpPut("box")]
    [Consumes("application/json")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(BoxDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateBox([FromBody] UpdateBoxRequest request)
    {
        var command = new UpdateBoxCommand(request);
        var result = await _mediator.Send(command);
        return result.ToActionResult(this);
    }
    
    /// <summary>
    /// Exclui um box existente
    /// </summary>
    /// <param name="request">ID do box a ser excluído e de sua respectiva casa/prédio</param>
    /// <returns>Retorna sucesso da operação</returns>
    [HttpDelete("box")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(DeleteBoxResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteBox([FromBody] DeleteBoxRequest request)
    {
        var command = new DeleteBoxCommand(request);
        var result = await _mediator.Send(command);
        return result.ToActionResult(this);
    }
    
    /// <summary>
    /// Atualiza uma casa/prédio existente
    /// </summary>
    /// <param name="id">ID da casa/prédio</param>
    /// <param name="request">Dados da casa/prédio a ser atualizada</param>
    /// <returns>Retorna os dados da casa/prédio atualizada</returns>
    [HttpPut]
    [Consumes("application/json")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(HouseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateHouse([FromBody] UpdateHouseRequest request)
    {
        var command = new UpdateHouseCommand(request);
        var result = await _mediator.Send(command);
        return result.ToActionResult(this);
    }
    
    /// <summary>
    /// Exclui uma casa/prédio existente
    /// </summary>
    /// <param name="id">ID da casa/prédio</param>
    /// <returns>Retorna sucesso da operação</returns>
    [HttpDelete("{id}")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(DeleteHouseResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteHouse(Guid id)
    {
        var request = new DeleteHouseRequest { Id = id };
        var command = new DeleteHouseCommand(request);
        var result = await _mediator.Send(command);
        return result.ToActionResult(this);
    }
}
