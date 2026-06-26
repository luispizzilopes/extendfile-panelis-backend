using ErrorOr;
using ExtendFile.Panelis.Application.Modules.User.Responses.TwoFactor;
using MediatR;

namespace ExtendFile.Panelis.Application.Modules.User.Commands.DisableTwoFactor;

public record DisableTwoFactorCommand(string Email) : IRequest<ErrorOr<DisableTwoFactorResponse>>;
