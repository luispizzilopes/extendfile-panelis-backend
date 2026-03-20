using ErrorOr;
using ExtendFile.Panelis.Application.Modules.Test.Requests.GetTestLinesByCatWithoutEating;
using ExtendFile.Panelis.Application.Modules.Test.Responses.GetTestLinesByCatWithoutEating;
using ExtendFile.Panelis.Application.Modules.Test.Responses;
using ExtendFile.Panelis.Domain.Interfaces.UnitOfWork;

namespace ExtendFile.Panelis.Application.Modules.Test.UseCases.GetTestLinesByCatWithoutEating;

public class GetTestLinesByCatWithoutEatingUseCase
{
    private readonly IUnitOfWork _unitOfWork;

    public GetTestLinesByCatWithoutEatingUseCase(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<GetTestLinesByCatWithoutEatingResponse>> ExecuteAsync(GetTestLinesByCatWithoutEatingRequest request, CancellationToken cancellationToken = default)
    {
        var cat = await _unitOfWork.CatRepository.GetCatByIdAsync(request.CatId, cancellationToken);
        
        if (cat is null)
            return Error.NotFound("Gato não encontrado", "Gato não encontrado");
        
        var testLines = await _unitOfWork.TestRepository
            .GetTestLinesByCatIdAsync(request.CatId, cancellationToken, cat.DaysWithoutEating);

        if (testLines is null)
            return Error.NotFound("Linhas de teste não encontradas", "Não foram encontradas linhas de teste para este gato");

        var testLineDtos = testLines.Select(line => new TestLineDto
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

        return new GetTestLinesByCatWithoutEatingResponse
        {
            TestLines = testLineDtos,
            DaysWithoutEating = cat.DaysWithoutEating
        };
    }
}