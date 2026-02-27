using ErrorOr;
using ExtendFile.Panelis.Application.Modules.Cat.Responses;
using ExtendFile.Panelis.Application.Modules.Cat.UseCases.UpdateCat;
using MediatR;

namespace ExtendFile.Panelis.Application.Modules.Cat.Commands.UpdateCat;

public class UpdateCatCommandHandler : IRequestHandler<UpdateCatCommand, ErrorOr<CatDto>>
{
    private readonly UpdateCatUseCase _updateCatUseCase;

    public UpdateCatCommandHandler(UpdateCatUseCase updateCatUseCase)
    {
        _updateCatUseCase = updateCatUseCase;
    }

    public async Task<ErrorOr<CatDto>> Handle(UpdateCatCommand request, CancellationToken cancellationToken)
    {
        return await _updateCatUseCase.ExecuteAsync(request.Request, cancellationToken);
    }
}
