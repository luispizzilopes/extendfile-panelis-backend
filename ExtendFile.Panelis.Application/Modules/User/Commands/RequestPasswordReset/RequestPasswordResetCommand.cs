using ErrorOr;
using ExtendFile.Panelis.Application.Modules.User.Requests.RequestPasswordReset;
using ExtendFile.Panelis.Application.Modules.User.Responses.RequestPasswordReset;
using MediatR;

namespace ExtendFile.Panelis.Application.Modules.User.Commands.RequestPasswordReset;

public record RequestPasswordResetCommand(
    RequestPasswordResetRequest Request
) : IRequest<ErrorOr<RequestPasswordResetResponse>>;
