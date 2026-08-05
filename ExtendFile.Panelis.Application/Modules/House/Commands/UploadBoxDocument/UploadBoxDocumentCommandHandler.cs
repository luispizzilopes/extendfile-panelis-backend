using ErrorOr;
using ExtendFile.Panelis.Application.Modules.House.Responses.UploadBoxDocument;
using ExtendFile.Panelis.Application.Modules.House.UseCases.UploadBoxDocument;
using MediatR;

namespace ExtendFile.Panelis.Application.Modules.House.Commands.UploadBoxDocument;

public class UploadBoxDocumentCommandHandler : IRequestHandler<UploadBoxDocumentCommand, ErrorOr<UploadBoxDocumentResponse>>
{
    private readonly UploadBoxDocumentUseCase _uploadBoxDocumentUseCase;

    public UploadBoxDocumentCommandHandler(UploadBoxDocumentUseCase uploadBoxDocumentUseCase)
    {
        _uploadBoxDocumentUseCase = uploadBoxDocumentUseCase;
    }

    public async Task<ErrorOr<UploadBoxDocumentResponse>> Handle(UploadBoxDocumentCommand request, CancellationToken cancellationToken)
    {
        return await _uploadBoxDocumentUseCase.ExecuteAsync(request.Request, cancellationToken);
    }
}
