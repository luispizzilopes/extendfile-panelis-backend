using ErrorOr;
using ExtendFile.Panelis.Application.Modules.Test.Requests.GetTestLinesByTestId;
using ExtendFile.Panelis.Application.Modules.Test.Responses;
using ExtendFile.Panelis.Domain.Interfaces.UnitOfWork;

namespace ExtendFile.Panelis.Application.Modules.Test.UseCases.GetTestLinesByTestId;

public class GetTestLinesByTestIdUseCase
{
    private readonly IUnitOfWork _unitOfWork;

    public GetTestLinesByTestIdUseCase(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<List<TestLineDto>>> ExecuteAsync(GetTestLinesByTestIdRequest request, CancellationToken cancellationToken = default)
    {
        var test = await _unitOfWork.TestRepository.GetTestByIdAsync(request.TestId, cancellationToken);
        
        if (test is null)
            return Error.NotFound("Teste não encontrado", "Teste não encontrado");

        var testLines = await _unitOfWork.TestRepository.GetTestLinesByTestIdAsync(request.TestId, cancellationToken);
        
        var testLineDtos = testLines?.Select(line => new TestLineDto
        {
            Id = line.Id.Value,
            Position = line.Position,
            CatName = line.CatName,
            CatId = line.CatId.Value,
            CatHash = line.CatHash,
            FirstFood = line.FirstFood,
            SecondFood = line.SecondFood,
            TotalAmountFood = line.TotalAmountFood,
            FoodAmountStatus = line.FoodAmountStatus
        }).ToList();

        return testLineDtos;
    }
}
