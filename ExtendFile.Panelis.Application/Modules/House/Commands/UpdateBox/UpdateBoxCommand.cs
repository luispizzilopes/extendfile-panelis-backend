using ErrorOr;
using ExtendFile.Panelis.Application.Modules.House.Requests.UpdateBox;
using ExtendFile.Panelis.Application.Modules.House.Responses;
using MediatR;

namespace ExtendFile.Panelis.Application.Modules.House.Commands.UpdateBox;

public record UpdateBoxCommand(UpdateBoxRequest Request) : IRequest<ErrorOr<BoxDto>>;
