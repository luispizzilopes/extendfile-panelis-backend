using ErrorOr;
using ExtendFile.Panelis.Application.Modules.User.Responses;
using ExtendFile.Panelis.Application.Modules.User.UseCases.Login;
using MediatR;

namespace ExtendFile.Panelis.Application.Modules.User.Commands.Login;

public class LoginCommandHandler : IRequestHandler<LoginCommand, ErrorOr<UserSessionResponse>>
{
    private readonly LoginUseCase _loginUseCase;

    public LoginCommandHandler(LoginUseCase loginUseCase)
    {
        _loginUseCase = loginUseCase;
    }

    public async Task<ErrorOr<UserSessionResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        return await _loginUseCase.ExecuteAsync(request.Request, cancellationToken);
    }
}
