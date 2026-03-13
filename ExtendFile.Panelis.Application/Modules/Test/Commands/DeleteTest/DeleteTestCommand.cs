using ErrorOr;
using ExtendFile.Panelis.Application.Modules.Test.Requests.DeleteTest;
using ExtendFile.Panelis.Application.Modules.Test.Responses.DeleteTest;
using MediatR;

namespace ExtendFile.Panelis.Application.Modules.Test.Commands.DeleteTest;

public record DeleteTestCommand(DeleteTestRequest Request) : IRequest<ErrorOr<DeleteTestResponse>>;
