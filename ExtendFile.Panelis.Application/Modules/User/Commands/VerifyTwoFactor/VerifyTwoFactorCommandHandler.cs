using ErrorOr;
using ExtendFile.Panelis.Application.Modules.User.Responses;
using ExtendFile.Panelis.Application.Modules.User.UseCases.VerifyTwoFactor;
using MediatR;

namespace ExtendFile.Panelis.Application.Modules.User.Commands.VerifyTwoFactor;

public class VerifyTwoFactorCommandHandler : IRequestHandler<VerifyTwoFactorCommand, ErrorOr<UserSessionResponse>>
{
    private readonly VerifyTwoFactorUseCase _useCase;

    public VerifyTwoFactorCommandHandler(VerifyTwoFactorUseCase useCase)
    {
        _useCase = useCase;
    }

    public async Task<ErrorOr<UserSessionResponse>> Handle(VerifyTwoFactorCommand request, CancellationToken cancellationToken)
    {
        return await _useCase.ExecuteAsync(request.Request, cancellationToken);
    }
}
