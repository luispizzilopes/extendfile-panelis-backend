using ErrorOr;
using ExtendFile.Panelis.Application.Modules.Test.Requests.GetTestsByBoxId;
using ExtendFile.Panelis.Application.Modules.Test.Responses;
using ExtendFile.Panelis.BuildingBlocks.Pagination;
using ExtendFile.Panelis.Domain.Interfaces.UnitOfWork;

namespace ExtendFile.Panelis.Application.Modules.Test.UseCases.GetTestsByBoxId;

public class GetTestsByBoxIdUseCase
{
    private readonly IUnitOfWork _unitOfWork;

    public GetTestsByBoxIdUseCase(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<PaginedResult<TestDto>>> ExecuteAsync(GetTestsByBoxIdRequest request, CancellationToken cancellationToken = default)
    {
        var testsResult = await _unitOfWork.TestRepository.GetTestsByBoxIdAsync(request.BoxId, request.PaginationParams, cancellationToken);
        
        var testDtos = testsResult.Data?.Select(test => new TestDto
        {
            Id = test.Id.Value,
            Name = test.Name,
            FileName = test.FileName,
            BoxId = test.BoxId.Value,
            TestDate = test.TestDate,
            CreatedAt = test.CreatedAt
        }).ToList();
        
        foreach (var testDto in testDtos)
        {
            var house = await _unitOfWork.HouseRepository.GetHouseByBoxIdAsync(testDto.BoxId, cancellationToken);
            testDto.HouseName = house?.Name ?? string.Empty;
            testDto.BoxName = house?.Boxes?.Where(x => x.Id.Value == testDto.BoxId).FirstOrDefault()?.Name ?? string.Empty;
        }

        return new PaginedResult<TestDto>
        {
            Data = testDtos,
            PageNumber = testsResult.PageNumber,
            PageSize = testsResult.PageSize,
            TotalRecords = testsResult.TotalRecords
        };
    }
}
