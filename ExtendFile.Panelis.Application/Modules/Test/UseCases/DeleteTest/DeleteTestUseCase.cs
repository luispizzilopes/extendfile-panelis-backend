using ErrorOr;
using ExtendFile.Panelis.Application.Modules.Test.Requests.DeleteTest;
using ExtendFile.Panelis.Application.Modules.Test.Responses.DeleteTest;
using ExtendFile.Panelis.Domain.Interfaces.UnitOfWork;

namespace ExtendFile.Panelis.Application.Modules.Test.UseCases.DeleteTest;

public class DeleteTestUseCase
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteTestUseCase(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<DeleteTestResponse>> ExecuteAsync(DeleteTestRequest request, CancellationToken cancellationToken)
    {
        var test = await _unitOfWork.TestRepository.GetTestByIdAsync(request.TestId, cancellationToken);
        
        if (test is null)
            return Error.NotFound("Teste não encontrado.", "Teste não encontrado.");
        
        var lastTest = await _unitOfWork.TestRepository.GetLastTestOrDefaultByBoxIdAsync(test.BoxId.Value, cancellationToken);
        
        if (lastTest is null || lastTest.Id != test.Id)
            return Error.Validation("Operação não permitida.", "Apenas o último teste lançado pode ser excluído.");
        
        await _unitOfWork.TestRepository.DeleteTestAsync(request.TestId, cancellationToken);
        await _unitOfWork.CommitAsync(cancellationToken);

        return new DeleteTestResponse
        {
            Success = true,
            Message = "Teste excluído com sucesso."
        };
    }
}
