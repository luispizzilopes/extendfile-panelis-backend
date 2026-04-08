using ErrorOr;
using ExtendFile.Panelis.Application.Modules.User.Requests.ResetPassword;
using ExtendFile.Panelis.Application.Modules.User.Responses.ResetPassword;
using MediatR;

namespace ExtendFile.Panelis.Application.Modules.User.Commands.ResetPassword;

public record ResetPasswordCommand(
    ResetPasswordRequest Request, 
    string Email
) : IRequest<ErrorOr<ResetPasswordResponse>>;  