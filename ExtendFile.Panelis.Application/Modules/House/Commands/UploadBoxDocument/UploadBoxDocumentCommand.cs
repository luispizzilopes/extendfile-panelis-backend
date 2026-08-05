using ErrorOr;
using ExtendFile.Panelis.Application.Modules.House.Requests.UploadBoxDocument;
using ExtendFile.Panelis.Application.Modules.House.Responses.UploadBoxDocument;
using MediatR;

namespace ExtendFile.Panelis.Application.Modules.House.Commands.UploadBoxDocument;

public record UploadBoxDocumentCommand(UploadBoxDocumentRequest Request) : IRequest<ErrorOr<UploadBoxDocumentResponse>>;
