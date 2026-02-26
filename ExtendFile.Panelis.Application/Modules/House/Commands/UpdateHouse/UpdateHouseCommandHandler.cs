using ErrorOr;
using ExtendFile.Panelis.Application.Modules.House.Responses;
using ExtendFile.Panelis.Application.Modules.House.UseCases.UpdateHouse;
using MediatR;

namespace ExtendFile.Panelis.Application.Modules.House.Commands.UpdateHouse;

public class UpdateHouseCommandHandler : IRequestHandler<UpdateHouseCommand, ErrorOr<HouseDto>>
{
    private readonly UpdateHouseUseCase _updateHouseUseCase;

    public UpdateHouseCommandHandler(UpdateHouseUseCase updateHouseUseCase)
    {
        _updateHouseUseCase = updateHouseUseCase;
    }

    public async Task<ErrorOr<HouseDto>> Handle(UpdateHouseCommand request, CancellationToken cancellationToken)
    {
        return await _updateHouseUseCase.ExecuteAsync(request.Request, cancellationToken);
    }
}
