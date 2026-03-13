using ErrorOr;
using ExtendFile.Panelis.Application.Modules.Test.Responses.DeleteTest;
using ExtendFile.Panelis.Application.Modules.Test.UseCases.DeleteTest;
using MediatR;

namespace ExtendFile.Panelis.Application.Modules.Test.Commands.DeleteTest;

public class DeleteTestCommandHandler : IRequestHandler<DeleteTestCommand, ErrorOr<DeleteTestResponse>>
{
    private readonly DeleteTestUseCase _deleteTestUseCase;

    public DeleteTestCommandHandler(DeleteTestUseCase deleteTestUseCase)
    {
        _deleteTestUseCase = deleteTestUseCase;
    }

    public async Task<ErrorOr<DeleteTestResponse>> Handle(DeleteTestCommand request, CancellationToken cancellationToken)
    {
        return await _deleteTestUseCase.ExecuteAsync(request.Request, cancellationToken);
    }
}
