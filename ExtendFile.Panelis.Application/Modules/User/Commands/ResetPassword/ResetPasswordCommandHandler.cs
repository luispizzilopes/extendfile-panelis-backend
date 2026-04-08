using ErrorOr;
using ExtendFile.Panelis.Application.Modules.User.Responses.ResetPassword;
using ExtendFile.Panelis.Application.Modules.User.UseCases.ResetPassword;
using MediatR;

namespace ExtendFile.Panelis.Application.Modules.User.Commands.ResetPassword;

public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, ErrorOr<ResetPasswordResponse>>
{
    private readonly ResetPasswordUseCase _resetPasswordUseCase;

    public ResetPasswordCommandHandler(ResetPasswordUseCase resetPasswordUseCase)
    {
        _resetPasswordUseCase = resetPasswordUseCase;
    }

    public async Task<ErrorOr<ResetPasswordResponse>> Handle(ResetPasswordCommand command, CancellationToken cancellationToken)
    {
        return await _resetPasswordUseCase.ExecuteAsync(command.Request, command.Email, cancellationToken);
    }
}