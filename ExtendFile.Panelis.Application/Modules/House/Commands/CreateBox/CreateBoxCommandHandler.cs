using ErrorOr;
using ExtendFile.Panelis.Application.Modules.House.Responses;
using ExtendFile.Panelis.Application.Modules.House.UseCases.CreateBox;
using MediatR;

namespace ExtendFile.Panelis.Application.Modules.House.Commands.CreateBox;

public class CreateBoxCommandHandler : IRequestHandler<CreateBoxCommand, ErrorOr<BoxDto>>
{
    private readonly CreateBoxUseCase _createBoxUseCase;

    public CreateBoxCommandHandler(CreateBoxUseCase createBoxUseCase)
    {
        _createBoxUseCase = createBoxUseCase;
    }

    public async Task<ErrorOr<BoxDto>> Handle(CreateBoxCommand request, CancellationToken cancellationToken)
    {
        return await _createBoxUseCase.ExecuteAsync(request.Request, cancellationToken);
    }
}