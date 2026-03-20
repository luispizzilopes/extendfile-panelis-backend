using ErrorOr;
using ExtendFile.Panelis.Application.Modules.Test.Responses.GetTestLinesByCatWithoutEating;
using ExtendFile.Panelis.Application.Modules.Test.UseCases.GetTestLinesByCatWithoutEating;
using MediatR;

namespace ExtendFile.Panelis.Application.Modules.Test.Queries.GetTestLinesByCatWithoutEating;

public class GetTestLinesByCatWithoutEatingQueryHandler : IRequestHandler<GetTestLinesByCatWithoutEatingQuery, ErrorOr<GetTestLinesByCatWithoutEatingResponse>>
{
    private readonly GetTestLinesByCatWithoutEatingUseCase _getTestLinesByCatWithoutEatingUseCase;

    public GetTestLinesByCatWithoutEatingQueryHandler(GetTestLinesByCatWithoutEatingUseCase getTestLinesByCatWithoutEatingUseCase)
    {
        _getTestLinesByCatWithoutEatingUseCase = getTestLinesByCatWithoutEatingUseCase;
    }
    
    public async Task<ErrorOr<GetTestLinesByCatWithoutEatingResponse>> Handle(GetTestLinesByCatWithoutEatingQuery request, CancellationToken cancellationToken)
    {
        return await _getTestLinesByCatWithoutEatingUseCase.ExecuteAsync(request.Request, cancellationToken);
    }
}
