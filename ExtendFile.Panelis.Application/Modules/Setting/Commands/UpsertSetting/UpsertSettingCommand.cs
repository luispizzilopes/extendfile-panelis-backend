using ErrorOr;
using ExtendFile.Panelis.Application.Modules.Setting.Requests.UpsertSetting;
using ExtendFile.Panelis.Application.Modules.Setting.Responses;
using MediatR;

namespace ExtendFile.Panelis.Application.Modules.Setting.Commands.UpsertSetting;

public record UpsertSettingCommand(UpsertSettingRequest Request) : IRequest<ErrorOr<SettingDto>>;
