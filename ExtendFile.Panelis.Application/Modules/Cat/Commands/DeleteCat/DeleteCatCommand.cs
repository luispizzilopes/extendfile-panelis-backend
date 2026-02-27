using ErrorOr;
using ExtendFile.Panelis.Application.Modules.Cat.Requests.DeleteCat;
using ExtendFile.Panelis.Application.Modules.Cat.Responses;
using ExtendFile.Panelis.Application.Modules.Cat.Responses.DeleteCat;
using MediatR;

namespace ExtendFile.Panelis.Application.Modules.Cat.Commands.DeleteCat;

public record DeleteCatCommand(DeleteCatRequest Request) : IRequest<ErrorOr<DeleteCatResponse>>;
