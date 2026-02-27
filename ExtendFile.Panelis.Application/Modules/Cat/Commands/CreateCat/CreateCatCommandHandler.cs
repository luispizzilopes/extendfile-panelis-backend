using ErrorOr;
using ExtendFile.Panelis.Application.Modules.Cat.Responses;
using ExtendFile.Panelis.Application.Modules.Cat.UseCases.CreateCat;
using MediatR;

namespace ExtendFile.Panelis.Application.Modules.Cat.Commands.CreateCat;

public class CreateCatCommandHandler : IRequestHandler<CreateCatCommand, ErrorOr<CatDto>>
{
    private readonly CreateCatUseCase _createCatUseCase;

    public CreateCatCommandHandler(CreateCatUseCase createCatUseCase)
    {
        _createCatUseCase = createCatUseCase;
    }

    public async Task<ErrorOr<CatDto>> Handle(CreateCatCommand request, CancellationToken cancellationToken)
    {
        return await _createCatUseCase.ExecuteAsync(request.Request, cancellationToken);
    }
}
