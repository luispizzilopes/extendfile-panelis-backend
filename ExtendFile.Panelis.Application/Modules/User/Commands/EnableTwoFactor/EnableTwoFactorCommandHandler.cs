using ErrorOr;
using ExtendFile.Panelis.Application.Modules.User.Responses.TwoFactor;
using ExtendFile.Panelis.Application.Modules.User.UseCases.EnableTwoFactor;
using MediatR;

namespace ExtendFile.Panelis.Application.Modules.User.Commands.EnableTwoFactor;

public class EnableTwoFactorCommandHandler : IRequestHandler<EnableTwoFactorCommand, ErrorOr<EnableTwoFactorResponse>>
{
    private readonly EnableTwoFactorUseCase _useCase;

    public EnableTwoFactorCommandHandler(EnableTwoFactorUseCase useCase)
    {
        _useCase = useCase;
    }

    public async Task<ErrorOr<EnableTwoFactorResponse>> Handle(EnableTwoFactorCommand request, CancellationToken cancellationToken)
    {
        return await _useCase.ExecuteAsync(request.Email, cancellationToken);
    }
}
