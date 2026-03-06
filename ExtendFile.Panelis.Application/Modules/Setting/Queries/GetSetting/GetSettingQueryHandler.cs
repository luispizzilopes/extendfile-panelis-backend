using ErrorOr;
using ExtendFile.Panelis.Application.Modules.Setting.Responses;
using ExtendFile.Panelis.Application.Modules.Setting.UseCases.GetSetting;
using MediatR;

namespace ExtendFile.Panelis.Application.Modules.Setting.Queries.GetSetting;

public class GetSettingQueryHandler : IRequestHandler<GetSettingQuery, ErrorOr<SettingDto>>
{
    private readonly GetSettingUseCase _getSettingUseCase;

    public GetSettingQueryHandler(GetSettingUseCase getSettingUseCase)
    {
        _getSettingUseCase = getSettingUseCase;
    }

    public async Task<ErrorOr<SettingDto>> Handle(GetSettingQuery request, CancellationToken cancellationToken)
    {
        return await _getSettingUseCase.ExecuteAsync(cancellationToken);
    }
}
