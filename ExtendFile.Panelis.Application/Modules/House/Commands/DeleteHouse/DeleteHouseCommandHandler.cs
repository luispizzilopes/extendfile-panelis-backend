using ErrorOr;
using ExtendFile.Panelis.Application.Modules.House.Responses.DeleteHouse;
using ExtendFile.Panelis.Application.Modules.House.UseCases.DeleteHouse;
using MediatR;

namespace ExtendFile.Panelis.Application.Modules.House.Commands.DeleteHouse;

public class DeleteHouseCommandHandler : IRequestHandler<DeleteHouseCommand, ErrorOr<DeleteHouseResponse>>
{
    private readonly DeleteHouseUseCase _deleteHouseUseCase;

    public DeleteHouseCommandHandler(DeleteHouseUseCase deleteHouseUseCase)
    {
        _deleteHouseUseCase = deleteHouseUseCase;
    }

    public async Task<ErrorOr<DeleteHouseResponse>> Handle(DeleteHouseCommand request, CancellationToken cancellationToken)
    {
        return await _deleteHouseUseCase.ExecuteAsync(request.Request, cancellationToken);
    }
}
