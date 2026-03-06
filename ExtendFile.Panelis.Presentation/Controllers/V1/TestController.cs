using ExtendFile.Panelis.Application.Modules.Test.Commands.CreateTest;
using ExtendFile.Panelis.Application.Modules.Test.Requests.CreateTest;
using ExtendFile.Panelis.Application.Modules.Test.Responses.CreateTest;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using ExtendFile.Panelis.Domain.Interfaces.Repositories.Test;
using ExtendFile.Panelis.Domain.Modules.Test.Aggregates;
using ExtendFile.Panelis.Presentation.Extensions;
using MediatR;

namespace ExtendFile.Panelis.Presentation.Controllers.V1;

/// <summary>
/// Controller para gerenciamento de operações relacionadas a Testes
/// </summary>
/// <remarks>
/// Este controller expõe endpoints para criação, consulta, atualização e exclusão de testes no sistema.
/// Todos os endpoints requerem autenticação via JWT Bearer.
/// </remarks>
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
[ApiController]
public class TestController : ControllerBase
{
    private readonly IMediator _mediator;
    
    public TestController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    /// <summary>
    /// Cria um novo teste no sistema
    /// </summary>
    /// <param name="request">Objeto contendo os dados do teste a ser criado</param>
    /// <returns>Retorna os dados do teste criado com seu identificador único</returns>
    /// <response code="201">Teste criado com sucesso</response>
    /// <response code="400">Dados inválidos ou requisição mal formatada</response>
    /// <response code="401">Usuário não autenticado</response>
    /// <response code="500">Erro interno do servidor</response>
    [HttpPost]
    [ProducesResponseType(typeof(CreateTestResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateTest([FromForm] CreateTestRequest request)
    {
        var createTestCommand = new CreateTestCommand(request);
        var result = await _mediator.Send(createTestCommand);
        return result.ToActionResult(this); 
    }
}