using ErrorOr;
using ExtendFile.Panelis.Application.Modules.Test.Responses.CreateTest;
using ExtendFile.Panelis.Application.Modules.Test.UseCases.CreateTest;
using MediatR;

namespace ExtendFile.Panelis.Application.Modules.Test.Commands.CreateTest;

public class CreateTestCommandHandler : IRequestHandler<CreateTestCommand, ErrorOr<CreateTestResponse>>
{
    private readonly CreateTestUseCase _createTestUseCase;

    public CreateTestCommandHandler(CreateTestUseCase createTestUseCase)
    {
        _createTestUseCase = createTestUseCase;
    }

    public async Task<ErrorOr<CreateTestResponse>> Handle(CreateTestCommand request, CancellationToken cancellationToken)
    {
        return await _createTestUseCase.Execute(request.Request, cancellationToken);
    }
}