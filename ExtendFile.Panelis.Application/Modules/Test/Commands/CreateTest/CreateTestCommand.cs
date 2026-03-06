using ErrorOr;
using ExtendFile.Panelis.Application.Modules.Test.Requests.CreateTest;
using ExtendFile.Panelis.Application.Modules.Test.Responses.CreateTest;
using MediatR;

namespace ExtendFile.Panelis.Application.Modules.Test.Commands.CreateTest;

public record CreateTestCommand(CreateTestRequest Request) : IRequest<ErrorOr<CreateTestResponse>>;