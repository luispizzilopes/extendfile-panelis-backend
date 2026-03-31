using ErrorOr;
using ExtendFile.Panelis.Application.Modules.User.Requests;
using ExtendFile.Panelis.Application.Modules.User.Responses;
using ExtendFile.Panelis.Application.Modules.User.UseCases.UserCreate;
using MediatR;

namespace ExtendFile.Panelis.Application.Modules.User.Commands;

public class UserCreateCommandHandler : IRequestHandler<UserCreateCommand, ErrorOr<UserCreateResponse>>
{
    private readonly UserCreateUseCase _userCreateUseCase;

    public UserCreateCommandHandler(UserCreateUseCase userCreateUseCase)
    {
        _userCreateUseCase = userCreateUseCase;
    }

    public async Task<ErrorOr<UserCreateResponse>> Handle(UserCreateCommand command, CancellationToken cancellationToken)
    {
        return await _userCreateUseCase.ExecuteAsync(command.Request, cancellationToken);
    }
}
