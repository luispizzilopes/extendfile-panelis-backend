using MediatR;
using Microsoft.AspNetCore.Mvc;
using ExtendFile.Panelis.Application.Modules.Dashboard.Requests.GetDashboard;
using ExtendFile.Panelis.Application.Modules.Dashboard.Queries.GetDashboard;
using ExtendFile.Panelis.Application.Modules.Dashboard.Responses.GetDashboard;
using ExtendFile.Panelis.Application.Modules.Dashboard.Requests.GetCatsWithoutEating;
using ExtendFile.Panelis.Application.Modules.Dashboard.Queries.GetCatsWithoutEating;
using ExtendFile.Panelis.Application.Modules.Dashboard.Responses.GetCatsWithoutEating;
using ExtendFile.Panelis.Presentation.Extensions;
using Microsoft.AspNetCore.Authorization;

namespace ExtendFile.Panelis.Presentation.Controllers.V1;

/// <summary>
/// Controller para operações do Dashboard
/// </summary>
[Authorize]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
[ApiController]
public class DashboardController : ControllerBase
{
    private readonly IMediator _mediator;
    
    public DashboardController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    /// <summary>
    /// Obtém dados do dashboard
    /// </summary>
    /// <returns>Retorna os dados do dashboard com totais</returns>
    [HttpGet]
    [Produces("application/json")]
    [ProducesResponseType(typeof(DashboardResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetDashboard()
    {
        var request = new GetDashboardRequest();
        var query = new GetDashboardQuery(request);
        var result = await _mediator.Send(query);
        return result.ToActionResult(this);
    }

    /// <summary>
    /// Obtém gatos sem comer em estado de alerta e atenção
    /// </summary>
    /// <returns>Retorna lista de gatos sem comer com status de alerta ou atenção</returns>
    [HttpGet("cats-without-eating")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(GetCatsWithoutEatingResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetCatsWithoutEating()
    {
        var request = new GetCatsWithoutEatingRequest();
        var query = new GetCatsWithoutEatingQuery(request);
        var result = await _mediator.Send(query);
        return result.ToActionResult(this);
    }
}
