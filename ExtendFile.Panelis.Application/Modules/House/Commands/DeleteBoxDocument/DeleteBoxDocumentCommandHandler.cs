using ErrorOr;
using ExtendFile.Panelis.Application.Modules.House.Responses.DeleteBoxDocument;
using ExtendFile.Panelis.Application.Modules.House.UseCases.DeleteBoxDocument;
using MediatR;

namespace ExtendFile.Panelis.Application.Modules.House.Commands.DeleteBoxDocument;

public class DeleteBoxDocumentCommandHandler : IRequestHandler<DeleteBoxDocumentCommand, ErrorOr<DeleteBoxDocumentResponse>>
{
    private readonly DeleteBoxDocumentUseCase _deleteBoxDocumentUseCase;

    public DeleteBoxDocumentCommandHandler(DeleteBoxDocumentUseCase deleteBoxDocumentUseCase)
    {
        _deleteBoxDocumentUseCase = deleteBoxDocumentUseCase;
    }

    public async Task<ErrorOr<DeleteBoxDocumentResponse>> Handle(DeleteBoxDocumentCommand request, CancellationToken cancellationToken)
    {
        return await _deleteBoxDocumentUseCase.ExecuteAsync(request.Request, cancellationToken);
    }
}
