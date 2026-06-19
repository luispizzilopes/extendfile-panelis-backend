using ErrorOr;
using ExtendFile.Panelis.Application.Modules.User.Requests.VerifyPasswordResetCode;
using ExtendFile.Panelis.Application.Modules.User.Responses.VerifyPasswordResetCode;
using MediatR;

namespace ExtendFile.Panelis.Application.Modules.User.Commands.VerifyPasswordResetCode;

public record VerifyPasswordResetCodeCommand(
    VerifyPasswordResetCodeRequest Request
) : IRequest<ErrorOr<VerifyPasswordResetCodeResponse>>;
