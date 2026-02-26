using ErrorOr;
using ExtendFile.Panelis.Application.Modules.House.Requests.DeleteHouse;
using ExtendFile.Panelis.Application.Modules.House.Responses.DeleteHouse;
using MediatR;

namespace ExtendFile.Panelis.Application.Modules.House.Commands.DeleteHouse;

public record DeleteHouseCommand(DeleteHouseRequest Request) : IRequest<ErrorOr<DeleteHouseResponse>>;
