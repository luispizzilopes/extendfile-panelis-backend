using ErrorOr;
using ExtendFile.Panelis.Application.Modules.User.Requests;
using ExtendFile.Panelis.Application.Modules.User.Responses;
using ExtendFile.Panelis.Application.Modules.User.UseCases.UserUpdate;
using MediatR;

namespace ExtendFile.Panelis.Application.Modules.User.Commands;

public class UserUpdateCommandHandler : IRequestHandler<UserUpdateCommand, ErrorOr<UserUpdateResponse>>
{
    private readonly UserUpdateUseCase _userUpdateUseCase;

    public UserUpdateCommandHandler(UserUpdateUseCase userUpdateUseCase)
    {
        _userUpdateUseCase = userUpdateUseCase;
    }

    public async Task<ErrorOr<UserUpdateResponse>> Handle(UserUpdateCommand command, CancellationToken cancellationToken)
    {
        return await _userUpdateUseCase.ExecuteAsync(command.Request, cancellationToken);
    }
}
