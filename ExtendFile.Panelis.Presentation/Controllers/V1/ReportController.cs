using MediatR;
using Microsoft.AspNetCore.Mvc;
using ExtendFile.Panelis.Application.Modules.Report.Queries.GetCatFoodConsumption;
using ExtendFile.Panelis.Application.Modules.Report.Requests.GetCatFoodConsumption;
using ExtendFile.Panelis.Application.Modules.Report.Responses;
using ExtendFile.Panelis.Presentation.Extensions;
using Microsoft.AspNetCore.Authorization;

namespace ExtendFile.Panelis.Presentation.Controllers.V1;

/// <summary>
/// Controller para geração de relatórios
/// </summary>
[Authorize]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
[ApiController]
public class ReportController : ControllerBase
{
    private readonly IMediator _mediator;

    public ReportController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Relatório de consumo alimentar por gato em período
    /// </summary>
    /// <param name="catId">ID do gato</param>
    /// <param name="startDate">Data de início do período</param>
    /// <param name="endDate">Data de fim do período</param>
    /// <returns>Relatório com totais e entradas diárias de consumo</returns>
    [HttpGet("cat-food-consumption")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(CatFoodConsumptionReportDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetCatFoodConsumption(
        [FromQuery] Guid catId,
        [FromQuery] DateTime startDate,
        [FromQuery] DateTime endDate)
    {
        var request = new GetCatFoodConsumptionRequest
        {
            CatId = catId,
            StartDate = startDate,
            EndDate = endDate
        };

        var query = new GetCatFoodConsumptionQuery(request);
        var result = await _mediator.Send(query);
        return result.ToActionResult(this);
    }
}
