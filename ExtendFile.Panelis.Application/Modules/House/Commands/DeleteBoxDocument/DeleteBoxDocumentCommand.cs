using ErrorOr;
using ExtendFile.Panelis.Application.Modules.House.Requests.DeleteBoxDocument;
using ExtendFile.Panelis.Application.Modules.House.Responses.DeleteBoxDocument;
using MediatR;

namespace ExtendFile.Panelis.Application.Modules.House.Commands.DeleteBoxDocument;

public record DeleteBoxDocumentCommand(DeleteBoxDocumentRequest Request) : IRequest<ErrorOr<DeleteBoxDocumentResponse>>;
