using ErrorOr;
using ExtendFile.Panelis.Application.Modules.User.Requests.ConfirmPasswordReset;
using ExtendFile.Panelis.Application.Modules.User.Responses.ConfirmPasswordReset;
using MediatR;

namespace ExtendFile.Panelis.Application.Modules.User.Commands.ConfirmPasswordReset;

public record ConfirmPasswordResetCommand(
    ConfirmPasswordResetRequest Request
) : IRequest<ErrorOr<ConfirmPasswordResetResponse>>;
