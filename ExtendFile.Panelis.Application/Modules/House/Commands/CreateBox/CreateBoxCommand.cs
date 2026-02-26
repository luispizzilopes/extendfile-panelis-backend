using ErrorOr;
using ExtendFile.Panelis.Application.Modules.House.Requests.CreateBox;
using ExtendFile.Panelis.Application.Modules.House.Responses;
using MediatR;

namespace ExtendFile.Panelis.Application.Modules.House.Commands.CreateBox;

public record CreateBoxCommand(CreateBoxRequest Request) : IRequest<ErrorOr<BoxDto>>;