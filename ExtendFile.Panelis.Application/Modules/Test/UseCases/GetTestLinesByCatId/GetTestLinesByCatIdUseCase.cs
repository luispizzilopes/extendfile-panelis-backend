using ErrorOr;
using ExtendFile.Panelis.Application.Modules.Test.Requests.GetTestLinesByCatId;
using ExtendFile.Panelis.Application.Modules.Test.Responses;
using ExtendFile.Panelis.BuildingBlocks.Pagination;
using ExtendFile.Panelis.Domain.Interfaces.Repositories.Test;

namespace ExtendFile.Panelis.Application.Modules.Test.UseCases.GetTestLinesByCatId;

public class GetTestLinesByCatIdUseCase
{
    private readonly ITestRepository _testRepository;

    public GetTestLinesByCatIdUseCase(ITestRepository testRepository)
    {
        _testRepository = testRepository;
    }

    public async Task<ErrorOr<PaginedResult<TestLineDto>>> ExecuteAsync(GetTestLinesByCatIdRequest request, CancellationToken cancellationToken)
    {
        var paginedTestLines = await _testRepository.GetTestLinesByCatIdPaginatedAsync(
            request.CatId, 
            request.PaginationParams, 
            cancellationToken);
        
        if (paginedTestLines.Data is null || !paginedTestLines.Data.Any())
        {
            return Error.NotFound("TestLines.NotFound", "Nenhuma linha de teste encontrada para este gato");
        }

        var testLineDtos = paginedTestLines.Data.Select(x => 
        {
            var testLine = x.Line;
            return new TestLineDto
            {
                Id = testLine.Id.Value,
                Position = testLine.Position,
                CatName = testLine.CatName,
                CatId = testLine.CatId.Value,
                CatHash = testLine.CatHash,
                FirstFood = testLine.FirstFood,
                SecondFood = testLine.SecondFood,
                TotalAmountFood = testLine.TotalAmountFood,
                FoodAmountStatus = testLine.FoodAmountStatus,
                TestDate = x.TestDate
            };
        }).ToList();

        var result = new PaginedResult<TestLineDto>
        {
            Data = testLineDtos,
            TotalRecords = paginedTestLines.TotalRecords,
            PageNumber = paginedTestLines.PageNumber,
            PageSize = paginedTestLines.PageSize
        };

        return result;
    }
}
