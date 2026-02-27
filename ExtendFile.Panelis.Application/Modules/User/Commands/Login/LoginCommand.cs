using ErrorOr;
using ExtendFile.Panelis.Application.Modules.User.Requests.Login;
using ExtendFile.Panelis.Application.Modules.User.Responses;
using MediatR;

namespace ExtendFile.Panelis.Application.Modules.User.Commands.Login;

public record LoginCommand(LoginRequest Request) : IRequest<ErrorOr<UserSessionResponse>>;
