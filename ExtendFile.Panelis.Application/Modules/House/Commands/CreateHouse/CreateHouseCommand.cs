using ErrorOr;
using ExtendFile.Panelis.Application.Modules.House.Requests.CreateHouse;
using ExtendFile.Panelis.Application.Modules.House.Responses;
using MediatR;

namespace ExtendFile.Panelis.Application.Modules.House.Commands.CreateHouse;

public record CreateHouseCommand(CreateHouseRequest Request) : IRequest<ErrorOr<HouseDto>>;
