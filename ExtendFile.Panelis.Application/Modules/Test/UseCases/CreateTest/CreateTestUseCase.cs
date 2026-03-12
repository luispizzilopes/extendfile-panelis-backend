using ErrorOr;
using ExtendFile.Panelis.Application.Modules.Test.Requests.CreateTest;
using ExtendFile.Panelis.Application.Modules.Test.Responses.CreateTest;
using ExtendFile.Panelis.Application.Modules.Test.UseCases.ProcessTest;
using ExtendFile.Panelis.Application.Modules.Setting.UseCases.GetSetting;
using ExtendFile.Panelis.Application.Modules.Setting.Responses;
using ExtendFile.Panelis.BuildingBlocks.Pagination;
using ExtendFile.Panelis.Domain.Interfaces.UnitOfWork;
using ExtendFile.Panelis.Domain.Modules.Cat.Enums;
using ExtendFile.Panelis.Domain.Modules.House.ValueObject;
using ExtendFile.Panelis.Domain.Modules.Test.Entities;
using ExtendFile.Panelis.Domain.Modules.Test.Enums;
using Microsoft.AspNetCore.Http;

namespace ExtendFile.Panelis.Application.Modules.Test.UseCases.CreateTest;

public class CreateTestUseCase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ProcessTestUseCase _processTestUseCase;
    private readonly GetSettingUseCase _getSettingUseCase;

    public CreateTestUseCase(IUnitOfWork unitOfWork, ProcessTestUseCase processTestUseCase, GetSettingUseCase getSettingUseCase)
    {
        _unitOfWork = unitOfWork;
        _processTestUseCase = processTestUseCase;
        _getSettingUseCase = getSettingUseCase;
    }

    public async Task<ErrorOr<CreateTestResponse>> Execute(CreateTestRequest request, CancellationToken cancellationToken)
    {
        var box = await _unitOfWork.HouseRepository
            .GetBoxByIdAsync(request.BoxId, cancellationToken);

        if (box is null)
            return Error.NotFound("Box não encontrado", "Não foi possível encontrar o Box");

        var settingResult = await _getSettingUseCase.ExecuteAsync(cancellationToken);
        if (settingResult.IsError)
            return settingResult.Errors;

        var setting = settingResult.Value;

        var rawText = await ReadFileAsync(request.File);
        var parsedRecord = _processTestUseCase.Execute(rawText);

        var existingTestByFileName = await _unitOfWork.TestRepository
            .GetTestByFileNameAsync(request.File.FileName, cancellationToken);

        if (existingTestByFileName is not null)
            return Error.Validation("Arquivo duplicado", $"Já existe um teste com o arquivo '{request.File.FileName}'");

        var testDate = parsedRecord.TestDate.Date.ToUniversalTime();
        
        if (request.ValidateDateFile is true)
        {
            var lastTest = await _unitOfWork.TestRepository
                .GetLastTestOrDefaultByBoxIdAsync(request.BoxId, cancellationToken);

            if (lastTest is not null)
            {
                var expectedNextDate = lastTest.TestDate.Date.AddDays(1);

                if (testDate.Date != expectedNextDate)
                {
                    return Error.Validation(
                        "Data inválida",
                        $"O próximo teste deve ser do dia '{expectedNextDate:dd/MM/yyyy}', pois o último teste foi em '{lastTest.TestDate:dd/MM/yyyy}'."
                    );
                }
            }
        }

        var existingTestByDate = await _unitOfWork.TestRepository
            .GetTestByDateAndBoxIdAsync(testDate, request.BoxId, cancellationToken);

        if (existingTestByDate is not null)
            return Error.Validation("Data duplicada", $"Já existe um teste para a data '{testDate:dd/MM/yyyy}' neste box");

        var catsInBox = await GetCatHashesInBoxAsync(request.BoxId, cancellationToken);

        var (testLines, testLineResponses) = await BuildTestLinesFromParsedRecordAsync(
            parsedRecord, 
            request.BoxId, 
            setting, 
            cancellationToken);

        await AppendMissingCatLinesAsync(testLines, testLineResponses, catsInBox, cancellationToken);

        var test = CreateTestAggregate(request.File.FileName, request.BoxId, parsedRecord.TestDate, testLines);

        await _unitOfWork.TestRepository.CreateTestAsync(test, cancellationToken);
        await _unitOfWork.CommitAsync(cancellationToken);

        return new CreateTestResponse
        {
            TestResponse = new TestResponse
            {
                TestDate = parsedRecord.TestDate,
                Lines = testLineResponses
            }
        };
    }

    /// <summary>
    /// Lê o conteúdo do arquivo IFormFile e retorna como string.
    /// </summary>
    private static async Task<string> ReadFileAsync(IFormFile file)
    {
        using var reader = new StreamReader(file.OpenReadStream());
        return await reader.ReadToEndAsync();
    }

    /// <summary>
    /// Retorna a lista de hashes dos gatos presentes no box, respeitando a capacidade máxima.
    /// </summary>
    private async Task<List<string>?> GetCatHashesInBoxAsync(Guid boxId, CancellationToken cancellationToken)
    {
        var catsInBox = await _unitOfWork.CatRepository
            .GetCatsByBoxIdAsync(boxId, cancellationToken);

        return catsInBox?
            .Select(x => x.Hash)
            .ToList();
    }

    /// <summary>
    /// Percorre as linhas do registro processado e constrói as TestLines dos gatos encontrados no box.
    /// Gatos não encontrados ou de outro box são ignorados.
    /// </summary>
    private async Task<(List<TestLine> testLines, List<TestLineResponse> testLineResponses)> BuildTestLinesFromParsedRecordAsync(
        TestResponse parsedRecord,
        Guid boxId,
        SettingDto setting,
        CancellationToken cancellationToken)
    {
        var testLines = new List<TestLine>();
        var testLineResponses = new List<TestLineResponse>();

        for (int i = 0; i < parsedRecord.Lines.Count; i++)
        {
            var catInfo = parsedRecord.Lines[i];

            var cat = await _unitOfWork.CatRepository.GetCatByHashAsync(catInfo.CatHash, cancellationToken);
            if (cat is null)
                continue;

            if (cat.BoxId.Value != boxId)
                continue;

            var totalFood = catInfo.FirstFood + catInfo.SecondFood;
            
            if (totalFood == 0 && cat.Location == CatLocation.AtVeterinarian)
                continue;

            var foodAmountStatus = ResolveFoodAmountStatus(totalFood, setting.LessThanEnoughThreshold, setting.MoreThanEnoughThreshold);

            var testLine = TestLine.Create(
                position: i + 1,
                catName: cat.Name,
                catId: cat.Id,
                catHash: catInfo.CatHash,
                firstFood: catInfo.FirstFood,
                secondFood: catInfo.SecondFood,
                foodAmountStatus: foodAmountStatus);

            testLines.Add(testLine);
            testLineResponses.Add(MapToTestLineResponse(testLine));
        }

        return (testLines, testLineResponses);
    }

    /// <summary>
    /// Adiciona linhas com consumo zero para os gatos do box que não apareceram no arquivo processado.
    /// </summary>
    private async Task AppendMissingCatLinesAsync(
        List<TestLine> testLines,
        List<TestLineResponse> testLineResponses,
        List<string>? catsInBox,
        CancellationToken cancellationToken)
    {
        if (catsInBox is null)
            return;

        var processedHashes = testLines.Select(x => x.CatHash).ToHashSet();

        foreach (var catHash in catsInBox)
        {
            if (processedHashes.Contains(catHash))
                continue;

            var cat = await _unitOfWork.CatRepository.GetCatByHashAsync(catHash, cancellationToken);
            if (cat is null)
                continue;

            if (cat.Location == CatLocation.AtVeterinarian)
                continue;

            var testLine = TestLine.Create(
                position: testLines.Count + 1,
                catName: cat.Name,
                catId: cat.Id,
                catHash: catHash,
                firstFood: 0,
                secondFood: 0,
                foodAmountStatus: FoodAmountStatus.LessThanEnough);

            testLines.Add(testLine);
            testLineResponses.Add(MapToTestLineResponse(testLine));
        }
    }

    /// <summary>
    /// Cria o agregado Test com as linhas geradas e o associa ao box informado.
    /// </summary>
    private static Domain.Modules.Test.Aggregates.Test CreateTestAggregate(
        string fileName,
        Guid boxId,
        DateTime testDate,
        List<TestLine> testLines)
    {
        var test = Domain.Modules.Test.Aggregates.Test.Create(
            name: $"Arquivo {testDate:dd/MM/yyyy}",
            fileName: fileName,
            boxId: BoxId.Create(boxId),
            testDate: testDate.Date.ToUniversalTime());

        test.AddLineRange(testLines);

        return test;
    }

    /// <summary>
    /// Define o status de consumo com base no total de alimento ingerido.
    /// Abaixo do threshold mínimo = pouco, acima do threshold máximo = muito, entre = suficiente.
    /// </summary>
    private static FoodAmountStatus ResolveFoodAmountStatus(decimal totalFood, decimal lessThanEnoughThreshold, decimal moreThanEnoughThreshold)
    {
        if (totalFood < lessThanEnoughThreshold)
            return FoodAmountStatus.LessThanEnough;
        
        if (totalFood > moreThanEnoughThreshold)
            return FoodAmountStatus.MoreThanEnough;
        
        return FoodAmountStatus.Enough;
    }

    /// <summary>
    /// Mapeia uma entidade TestLine para o objeto de resposta TestLineResponse.
    /// </summary>
    private static TestLineResponse MapToTestLineResponse(TestLine line) => new()
    {
        CatName = line.CatName,
        CatHash = line.CatHash,
        FirstFood = line.FirstFood,
        SecondFood = line.SecondFood,
        TotalAmountFood = line.TotalAmountFood,
        FoodAmountStatus = line.FoodAmountStatus
    };
}