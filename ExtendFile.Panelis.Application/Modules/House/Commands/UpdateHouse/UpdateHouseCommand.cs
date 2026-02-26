using ErrorOr;
using ExtendFile.Panelis.Application.Modules.House.Requests.UpdateHouse;
using ExtendFile.Panelis.Application.Modules.House.Responses;
using MediatR;

namespace ExtendFile.Panelis.Application.Modules.House.Commands.UpdateHouse;

public record UpdateHouseCommand(UpdateHouseRequest Request) : IRequest<ErrorOr<HouseDto>>;
