using ErrorOr;
using ExtendFile.Panelis.Application.Modules.User.Responses.ConfirmPasswordReset;
using ExtendFile.Panelis.Application.Modules.User.UseCases.ConfirmPasswordReset;
using MediatR;

namespace ExtendFile.Panelis.Application.Modules.User.Commands.ConfirmPasswordReset;

public class ConfirmPasswordResetCommandHandler : IRequestHandler<ConfirmPasswordResetCommand, ErrorOr<ConfirmPasswordResetResponse>>
{
    private readonly ConfirmPasswordResetUseCase _useCase;

    public ConfirmPasswordResetCommandHandler(ConfirmPasswordResetUseCase useCase)
    {
        _useCase = useCase;
    }

    public async Task<ErrorOr<ConfirmPasswordResetResponse>> Handle(ConfirmPasswordResetCommand command, CancellationToken cancellationToken)
    {
        return await _useCase.ExecuteAsync(command.Request, cancellationToken);
    }
}
