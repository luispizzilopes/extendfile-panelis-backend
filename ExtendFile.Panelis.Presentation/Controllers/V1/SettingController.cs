using ErrorOr;
using ExtendFile.Panelis.Application.Modules.Setting.Commands.UpsertSetting;
using ExtendFile.Panelis.Application.Modules.Setting.Queries.GetSetting;
using ExtendFile.Panelis.Application.Modules.Setting.Requests.UpsertSetting;
using ExtendFile.Panelis.Application.Modules.Setting.Responses;
using ExtendFile.Panelis.Presentation.Extensions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExtendFile.Panelis.Presentation.Controllers.V1;

/// <summary>
/// Controller para gerenciamento de configurações do sistema
/// </summary>
[Authorize]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
[ApiController]
public class SettingController : ControllerBase
{
    private readonly IMediator _mediator;

    /// <summary>
    /// Construtor do SettingController
    /// </summary>
    /// <param name="mediator">Instância do MediatR para envio de commands e queries</param>
    public SettingController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Busca as configurações atuais do sistema
    /// </summary>
    /// <param name="cancellationToken">Token de cancelamento da operação</param>
    /// <returns>Configurações atuais do sistema</returns>
    /// <response code="200">Retorna as configurações encontradas</response>
    /// <response code="404">Nenhuma configuração encontrada</response>
    /// <response code="401">Usuário não autenticado</response>
    /// <response code="500">Erro interno do servidor</response>
    [HttpGet]
    public async Task<IActionResult> GetSetting(CancellationToken cancellationToken)
    {
        var query = new GetSettingQuery();
        var result = await _mediator.Send(query, cancellationToken);
        return result.ToActionResult(this);
    }

    /// <summary>
    /// Cria ou atualiza as configurações do sistema (Upsert)
    /// </summary>
    /// <param name="request">Dados das configurações a serem criadas ou atualizadas</param>
    /// <param name="cancellationToken">Token de cancelamento da operação</param>
    /// <returns>Configurações criadas ou atualizadas</returns>
    /// <response code="200">Retorna as configurações criadas ou atualizadas com sucesso</response>
    /// <response code="400">Dados inválidos fornecidos</response>
    /// <response code="401">Usuário não autenticado</response>
    /// <response code="500">Erro interno do servidor</response>
    [HttpPost]
    public async Task<IActionResult> UpsertSetting(
        [FromBody] UpsertSettingRequest request, 
        CancellationToken cancellationToken)
    {
        var command = new UpsertSettingCommand(request);
        var result = await _mediator.Send(command, cancellationToken);
        return result.ToActionResult(this);
    }
}
