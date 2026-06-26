using ErrorOr;
using ExtendFile.Panelis.Application.Modules.User.Responses.TwoFactor;
using ExtendFile.Panelis.Application.Modules.User.UseCases.DisableTwoFactor;
using MediatR;

namespace ExtendFile.Panelis.Application.Modules.User.Commands.DisableTwoFactor;

public class DisableTwoFactorCommandHandler : IRequestHandler<DisableTwoFactorCommand, ErrorOr<DisableTwoFactorResponse>>
{
    private readonly DisableTwoFactorUseCase _useCase;

    public DisableTwoFactorCommandHandler(DisableTwoFactorUseCase useCase)
    {
        _useCase = useCase;
    }

    public async Task<ErrorOr<DisableTwoFactorResponse>> Handle(DisableTwoFactorCommand request, CancellationToken cancellationToken)
    {
        return await _useCase.ExecuteAsync(request.Email, cancellationToken);
    }
}
