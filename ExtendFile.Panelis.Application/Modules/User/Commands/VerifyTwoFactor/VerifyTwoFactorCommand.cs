using ErrorOr;
using ExtendFile.Panelis.Application.Modules.User.Requests.TwoFactor;
using ExtendFile.Panelis.Application.Modules.User.Responses;
using MediatR;

namespace ExtendFile.Panelis.Application.Modules.User.Commands.VerifyTwoFactor;

public record VerifyTwoFactorCommand(VerifyTwoFactorRequest Request) : IRequest<ErrorOr<UserSessionResponse>>;
