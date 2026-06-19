using ErrorOr;
using ExtendFile.Panelis.Application.Modules.User.Responses.VerifyPasswordResetCode;
using ExtendFile.Panelis.Application.Modules.User.UseCases.VerifyPasswordResetCode;
using MediatR;

namespace ExtendFile.Panelis.Application.Modules.User.Commands.VerifyPasswordResetCode;

public class VerifyPasswordResetCodeCommandHandler : IRequestHandler<VerifyPasswordResetCodeCommand, ErrorOr<VerifyPasswordResetCodeResponse>>
{
    private readonly VerifyPasswordResetCodeUseCase _useCase;

    public VerifyPasswordResetCodeCommandHandler(VerifyPasswordResetCodeUseCase useCase)
    {
        _useCase = useCase;
    }

    public async Task<ErrorOr<VerifyPasswordResetCodeResponse>> Handle(VerifyPasswordResetCodeCommand command, CancellationToken cancellationToken)
    {
        return await _useCase.ExecuteAsync(command.Request, cancellationToken);
    }
}
