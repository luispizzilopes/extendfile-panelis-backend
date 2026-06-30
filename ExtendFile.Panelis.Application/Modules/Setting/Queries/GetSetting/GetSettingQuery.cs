using ErrorOr;
using ExtendFile.Panelis.Application.Interfaces.Caching;
using ExtendFile.Panelis.Application.Modules.Setting.Responses;
using MediatR;

namespace ExtendFile.Panelis.Application.Modules.Setting.Queries.GetSetting;

public record GetSettingQuery : IRequest<ErrorOr<SettingDto>>; 
