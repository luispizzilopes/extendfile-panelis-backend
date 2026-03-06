using ErrorOr;
using ExtendFile.Panelis.Application.Modules.Setting.Responses;
using ExtendFile.Panelis.Application.Modules.Setting.UseCases.UpsertSetting;
using MediatR;

namespace ExtendFile.Panelis.Application.Modules.Setting.Commands.UpsertSetting;

public class UpsertSettingCommandHandler : IRequestHandler<UpsertSettingCommand, ErrorOr<SettingDto>>
{
    private readonly UpsertSettingUseCase _upsertSettingUseCase;

    public UpsertSettingCommandHandler(UpsertSettingUseCase upsertSettingUseCase)
    {
        _upsertSettingUseCase = upsertSettingUseCase;
    }

    public async Task<ErrorOr<SettingDto>> Handle(UpsertSettingCommand request, CancellationToken cancellationToken)
    {
        return await _upsertSettingUseCase.ExecuteAsync(request.Request, cancellationToken);
    }
}
