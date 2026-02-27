using ErrorOr;
using ExtendFile.Panelis.Application.Modules.Cat.Requests.CreateCat;
using ExtendFile.Panelis.Application.Modules.Cat.Responses;
using MediatR;

namespace ExtendFile.Panelis.Application.Modules.Cat.Commands.CreateCat;

public record CreateCatCommand(CreateCatRequest Request) : IRequest<ErrorOr<CatDto>>;
