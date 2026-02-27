using ErrorOr;
using ExtendFile.Panelis.Application.Modules.Cat.Responses;
using ExtendFile.Panelis.Application.Modules.Cat.UseCases.GetCatById;
using MediatR;

namespace ExtendFile.Panelis.Application.Modules.Cat.Queries.GetCatById;

public class GetCatByIdQueryHandler : IRequestHandler<GetCatByIdQuery, ErrorOr<CatDto>>
{
    private readonly GetCatByIdUseCase _getCatByIdUseCase;

    public GetCatByIdQueryHandler(GetCatByIdUseCase getCatByIdUseCase)
    {
        _getCatByIdUseCase = getCatByIdUseCase;
    }

    public async Task<ErrorOr<CatDto>> Handle(GetCatByIdQuery request, CancellationToken cancellationToken)
    {
        return await _getCatByIdUseCase.ExecuteAsync(request.Request, cancellationToken);
    }
}
