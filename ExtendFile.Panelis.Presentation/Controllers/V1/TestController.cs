using ExtendFile.Panelis.Application.Modules.Test.Commands.CreateTest;
using ExtendFile.Panelis.Application.Modules.Test.Commands.DeleteTest;
using ExtendFile.Panelis.Application.Modules.Test.Requests.CreateTest;
using ExtendFile.Panelis.Application.Modules.Test.Requests.DeleteTest;
using ExtendFile.Panelis.Application.Modules.Test.Responses.CreateTest;
using ExtendFile.Panelis.Application.Modules.Test.Responses.DeleteTest;
using ExtendFile.Panelis.Application.Modules.Test.Requests.GetTestsByBoxId;
using ExtendFile.Panelis.Application.Modules.Test.Requests.GetTestLinesByTestId;
using ExtendFile.Panelis.Application.Modules.Test.Requests.GetTestLinesByCatWithoutEating;
using ExtendFile.Panelis.Application.Modules.Test.Requests.GetTestLinesByCatId;
using ExtendFile.Panelis.Application.Modules.Test.Responses;
using ExtendFile.Panelis.Application.Modules.Test.Responses.GetTestLinesByCatWithoutEating;
using ExtendFile.Panelis.Application.Modules.Test.Queries.GetTestsByBoxId;
using ExtendFile.Panelis.Application.Modules.Test.Queries.GetTestLinesByTestId;
using ExtendFile.Panelis.Application.Modules.Test.Queries.GetTestLinesByCatWithoutEating;
using ExtendFile.Panelis.Application.Modules.Test.Queries.GetTestLinesByCatId;
using ExtendFile.Panelis.BuildingBlocks.Pagination;
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

    /// <summary>
    /// Obtém lista paginada de testes por Box ID
    /// </summary>
    /// <param name="boxId">ID do box</param>
    /// <param name="paginationParams">Parâmetros de paginação</param>
    /// <returns>Retorna lista paginada de testes do box especificado</returns>
    /// <response code="200">Testes encontrados com sucesso</response>
    /// <response code="400">Dados inválidos ou requisição mal formatada</response>
    /// <response code="401">Usuário não autenticado</response>
    /// <response code="500">Erro interno do servidor</response>
    [HttpGet("by-box/{boxId}")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(PaginedResult<TestDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetTestsByBoxId(
        Guid boxId,
        [FromQuery] PaginationParams paginationParams)
    {
        var request = new GetTestsByBoxIdRequest 
        { 
            BoxId = boxId,
            PaginationParams = paginationParams
        };
        var query = new GetTestsByBoxIdQuery(request);
        var result = await _mediator.Send(query);
        return result.ToActionResult(this);
    }

    /// <summary>
    /// Obtém todas as linhas de um teste específico
    /// </summary>
    /// <param name="testId">ID do teste</param>
    /// <returns>Retorna lista com todas as linhas do teste</returns>
    /// <response code="200">Linhas do teste encontradas com sucesso</response>
    /// <response code="404">Teste não encontrado</response>
    /// <response code="400">Dados inválidos ou requisição mal formatada</response>
    /// <response code="401">Usuário não autenticado</response>
    /// <response code="500">Erro interno do servidor</response>
    [HttpGet("{testId}/lines")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(List<TestLineDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetTestLinesByTestId(Guid testId)
    {
        var request = new GetTestLinesByTestIdRequest { TestId = testId };
        var query = new GetTestLinesByTestIdQuery(request);
        var result = await _mediator.Send(query);
        return result.ToActionResult(this);
    }

    /// <summary>
    /// Exclui um teste do sistema
    /// </summary>
    /// <param name="testId">ID do teste a ser excluído</param>
    /// <returns>Retorna confirmação da exclusão do teste</returns>
    /// <response code="200">Teste excluído com sucesso</response>
    /// <response code="404">Teste não encontrado</response>
    /// <response code="400">Dados inválidos ou requisição mal formatada</response>
    /// <response code="401">Usuário não autenticado</response>
    /// <response code="500">Erro interno do servidor</response>
    [HttpDelete("{testId}")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(DeleteTestResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteTest(Guid testId)
    {
        var request = new DeleteTestRequest { TestId = testId };
        var command = new DeleteTestCommand(request);
        var result = await _mediator.Send(command);
        return result.ToActionResult(this);
    }

    /// <summary>
    /// Obtém as linhas de teste de um gato e quantidade de dias sem comer
    /// </summary>
    /// <param name="catId">ID do gato</param>
    /// <returns>Retorna lista com as linhas de teste do gato e dias sem comer</returns>
    /// <response code="200">Linhas de teste encontradas com sucesso</response>
    /// <response code="404">Gato não encontrado</response>
    /// <response code="400">Dados inválidos ou requisição mal formatada</response>
    /// <response code="401">Usuário não autenticado</response>
    /// <response code="500">Erro interno do servidor</response>
    [HttpGet("lines/by-cat/{catId}/without-eating")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(GetTestLinesByCatWithoutEatingResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetTestLinesByCatWithoutEating(Guid catId)
    {
        var request = new GetTestLinesByCatWithoutEatingRequest { CatId = catId };
        var query = new GetTestLinesByCatWithoutEatingQuery(request);
        var result = await _mediator.Send(query);
        return result.ToActionResult(this);
    }

    /// <summary>
    /// Obtém as linhas de teste de um gato específico
    /// </summary>
    /// <param name="catId">ID do gato</param>
    /// <param name="paginationParams">Parâmetros de paginação</param>
    /// <returns>Retorna lista paginada com as linhas de teste do gato</returns>
    /// <response code="200">Linhas de teste encontradas com sucesso</response>
    /// <response code="404">Gato não encontrado ou sem linhas de teste</response>
    /// <response code="400">Dados inválidos ou requisição mal formatada</response>
    /// <response code="401">Usuário não autenticado</response>
    /// <response code="500">Erro interno do servidor</response>
    [HttpGet("lines/by-cat/{catId}")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(PaginedResult<TestLineDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetTestLinesByCatId(
        Guid catId,
        [FromQuery] PaginationParams paginationParams)
    {
        var request = new GetTestLinesByCatIdRequest 
        { 
            CatId = catId,
            PaginationParams = paginationParams
        };
        var query = new GetTestLinesByCatIdQuery(request);
        var result = await _mediator.Send(query);
        return result.ToActionResult(this);
    }
}