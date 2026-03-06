using ErrorOr;
using ExtendFile.Panelis.Application.Modules.Setting.Responses;
using ExtendFile.Panelis.Domain.Interfaces.UnitOfWork;

namespace ExtendFile.Panelis.Application.Modules.Setting.UseCases.GetSetting;

public class GetSettingUseCase
{
    private readonly IUnitOfWork _unitOfWork;

    public GetSettingUseCase(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<SettingDto>> ExecuteAsync(CancellationToken cancellationToken = default)
    {
        var setting = await _unitOfWork.SettingRepository.GetSettingAsync(cancellationToken);

        return new SettingDto
        {
            Id = setting is not null ? setting.Id.Value : Guid.Empty,
            LessThanEnoughThreshold = setting?.LessThanEnoughThreshold ?? 0,
            MoreThanEnoughThreshold = setting?.MoreThanEnoughThreshold ?? 0,
            CreatedAt = setting?.CreatedAt ?? DateTime.UtcNow,
            UpdatedAt = setting?.UpdatedAt,
        };
    }
}
