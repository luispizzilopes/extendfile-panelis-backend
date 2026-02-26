using ErrorOr;
using ExtendFile.Panelis.Application.Modules.House.Responses;
using ExtendFile.Panelis.Application.Modules.House.UseCases.CreateHouse;
using MediatR;

namespace ExtendFile.Panelis.Application.Modules.House.Commands.CreateHouse;

public class CreateHouseCommandHandler : IRequestHandler<CreateHouseCommand, ErrorOr<HouseDto>>
{
    private readonly CreateHouseUseCase _createHouseUseCase;

    public CreateHouseCommandHandler(CreateHouseUseCase createHouseUseCase)
    {
        _createHouseUseCase = createHouseUseCase;
    }

    public async Task<ErrorOr<HouseDto>> Handle(CreateHouseCommand request, CancellationToken cancellationToken)
    {
        return await _createHouseUseCase.ExecuteAsync(request.Request, cancellationToken);
    }
}
