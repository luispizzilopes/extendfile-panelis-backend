using ErrorOr;
using ExtendFile.Panelis.Application.Modules.Test.Responses;
using ExtendFile.Panelis.Application.Modules.Test.UseCases.GetTestLinesByTestId;
using MediatR;

namespace ExtendFile.Panelis.Application.Modules.Test.Queries.GetTestLinesByTestId;

public class GetTestLinesByTestIdQueryHandler : IRequestHandler<GetTestLinesByTestIdQuery, ErrorOr<List<TestLineDto>>>
{
    private readonly GetTestLinesByTestIdUseCase _getTestLinesByTestIdUseCase;

    public GetTestLinesByTestIdQueryHandler(GetTestLinesByTestIdUseCase getTestLinesByTestIdUseCase)
    {
        _getTestLinesByTestIdUseCase = getTestLinesByTestIdUseCase;
    }

    public async Task<ErrorOr<List<TestLineDto>>> Handle(GetTestLinesByTestIdQuery request, CancellationToken cancellationToken)
    {
        return await _getTestLinesByTestIdUseCase.ExecuteAsync(request.Request, cancellationToken);
    }
}
