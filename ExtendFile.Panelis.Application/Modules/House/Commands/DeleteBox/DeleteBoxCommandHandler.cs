using ErrorOr;
using ExtendFile.Panelis.Application.Modules.House.Responses.DeleteBox;
using ExtendFile.Panelis.Application.Modules.House.UseCases.DeleteBox;
using MediatR;

namespace ExtendFile.Panelis.Application.Modules.House.Commands.DeleteBox;

public class DeleteBoxCommandHandler : IRequestHandler<DeleteBoxCommand, ErrorOr<DeleteBoxResponse>>
{
    private readonly DeleteBoxUseCase _deleteBoxUseCase;

    public DeleteBoxCommandHandler(DeleteBoxUseCase deleteBoxUseCase)
    {
        _deleteBoxUseCase = deleteBoxUseCase;
    }

    public async Task<ErrorOr<DeleteBoxResponse>> Handle(DeleteBoxCommand request, CancellationToken cancellationToken)
    {
        return await _deleteBoxUseCase.ExecuteAsync(request.Request, cancellationToken);
    }
}
