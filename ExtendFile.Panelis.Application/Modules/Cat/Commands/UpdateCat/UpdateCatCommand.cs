using ErrorOr;
using ExtendFile.Panelis.Application.Modules.Cat.Requests.UpdateCat;
using ExtendFile.Panelis.Application.Modules.Cat.Responses;
using MediatR;

namespace ExtendFile.Panelis.Application.Modules.Cat.Commands.UpdateCat;

public record UpdateCatCommand(UpdateCatRequest Request) : IRequest<ErrorOr<CatDto>>;
