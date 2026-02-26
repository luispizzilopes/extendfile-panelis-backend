using ErrorOr;
using ExtendFile.Panelis.Application.Modules.House.Responses;
using ExtendFile.Panelis.Application.Modules.House.UseCases.UpdateBox;
using MediatR;

namespace ExtendFile.Panelis.Application.Modules.House.Commands.UpdateBox;

public class UpdateBoxCommandHandler : IRequestHandler<UpdateBoxCommand, ErrorOr<BoxDto>>
{
    private readonly UpdateBoxUseCase _updateBoxUseCase;

    public UpdateBoxCommandHandler(UpdateBoxUseCase updateBoxUseCase)
    {
        _updateBoxUseCase = updateBoxUseCase;
    }

    public async Task<ErrorOr<BoxDto>> Handle(UpdateBoxCommand request, CancellationToken cancellationToken)
    {
        return await _updateBoxUseCase.ExecuteAsync(request.Request, cancellationToken);
    }
}
