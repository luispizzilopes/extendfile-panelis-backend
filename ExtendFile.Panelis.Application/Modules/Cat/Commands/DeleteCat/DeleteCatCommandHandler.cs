using ErrorOr;
using ExtendFile.Panelis.Application.Modules.Cat.Responses.DeleteCat;
using ExtendFile.Panelis.Application.Modules.Cat.UseCases.DeleteCat;
using MediatR;

namespace ExtendFile.Panelis.Application.Modules.Cat.Commands.DeleteCat;

public class DeleteCatCommandHandler : IRequestHandler<DeleteCatCommand, ErrorOr<DeleteCatResponse>>
{
    private readonly DeleteCatUseCase _deleteCatUseCase;

    public DeleteCatCommandHandler(DeleteCatUseCase deleteCatUseCase)
    {
        _deleteCatUseCase = deleteCatUseCase;
    }

    public async Task<ErrorOr<DeleteCatResponse>> Handle(DeleteCatCommand request, CancellationToken cancellationToken)
    {
        return await _deleteCatUseCase.ExecuteAsync(request.Request, cancellationToken);
    }
}
