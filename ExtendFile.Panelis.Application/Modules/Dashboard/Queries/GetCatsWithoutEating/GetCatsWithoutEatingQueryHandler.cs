using ErrorOr;
using ExtendFile.Panelis.Application.Modules.Dashboard.Responses.GetCatsWithoutEating;
using ExtendFile.Panelis.Application.Modules.Dashboard.UseCases.GetCatsWithoutEating;
using MediatR;

namespace ExtendFile.Panelis.Application.Modules.Dashboard.Queries.GetCatsWithoutEating;

public class GetCatsWithoutEatingQueryHandler : IRequestHandler<GetCatsWithoutEatingQuery, ErrorOr<GetCatsWithoutEatingResponse>>
{
    private readonly GetCatsWithoutEatingUseCase _getCatsWithoutEatingUseCase;

    public GetCatsWithoutEatingQueryHandler(GetCatsWithoutEatingUseCase getCatsWithoutEatingUseCase)
    {
        _getCatsWithoutEatingUseCase = getCatsWithoutEatingUseCase;
    }

    public async Task<ErrorOr<GetCatsWithoutEatingResponse>> Handle(GetCatsWithoutEatingQuery request, CancellationToken cancellationToken)
    {
        return await _getCatsWithoutEatingUseCase.ExecuteAsync(request.Request, cancellationToken);
    }
}
