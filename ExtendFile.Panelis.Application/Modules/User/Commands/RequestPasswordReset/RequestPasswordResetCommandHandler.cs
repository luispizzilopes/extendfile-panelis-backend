using ErrorOr;
using ExtendFile.Panelis.Application.Modules.User.Responses.RequestPasswordReset;
using ExtendFile.Panelis.Application.Modules.User.UseCases.RequestPasswordReset;
using MediatR;

namespace ExtendFile.Panelis.Application.Modules.User.Commands.RequestPasswordReset;

public class RequestPasswordResetCommandHandler : IRequestHandler<RequestPasswordResetCommand, ErrorOr<RequestPasswordResetResponse>>
{
    private readonly RequestPasswordResetUseCase _useCase;

    public RequestPasswordResetCommandHandler(RequestPasswordResetUseCase useCase)
    {
        _useCase = useCase;
    }

    public async Task<ErrorOr<RequestPasswordResetResponse>> Handle(RequestPasswordResetCommand command, CancellationToken cancellationToken)
    {
        return await _useCase.ExecuteAsync(command.Request, cancellationToken);
    }
}
