using ErrorOr;
using ExtendFile.Panelis.Application.Modules.User.Responses.TwoFactor;
using MediatR;

namespace ExtendFile.Panelis.Application.Modules.User.Commands.EnableTwoFactor;

public record EnableTwoFactorCommand(string Email) : IRequest<ErrorOr<EnableTwoFactorResponse>>;
