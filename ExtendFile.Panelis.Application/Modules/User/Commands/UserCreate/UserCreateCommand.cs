using ErrorOr;
using ExtendFile.Panelis.Application.Modules.User.Requests;
using ExtendFile.Panelis.Application.Modules.User.Responses;
using MediatR;

namespace ExtendFile.Panelis.Application.Modules.User.Commands;

public record UserCreateCommand(UserCreateRequest Request) : IRequest<ErrorOr<UserCreateResponse>>;
