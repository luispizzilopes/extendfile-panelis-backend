using ErrorOr;
using ExtendFile.Panelis.Application.Modules.House.Requests.DeleteBox;
using ExtendFile.Panelis.Application.Modules.House.Responses.DeleteBox;
using MediatR;

namespace ExtendFile.Panelis.Application.Modules.House.Commands.DeleteBox;

public record DeleteBoxCommand(DeleteBoxRequest Request) : IRequest<ErrorOr<DeleteBoxResponse>>;
