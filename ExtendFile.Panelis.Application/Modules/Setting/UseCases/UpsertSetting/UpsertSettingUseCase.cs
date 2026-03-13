using ErrorOr;
using ExtendFile.Panelis.Application.Modules.Setting.Requests.UpsertSetting;
using ExtendFile.Panelis.Application.Modules.Setting.Responses;
using ExtendFile.Panelis.Domain.Interfaces.UnitOfWork;
using ExtendFile.Panelis.Domain.Modules.Setting.Aggregates;

namespace ExtendFile.Panelis.Application.Modules.Setting.UseCases.UpsertSetting;

public class UpsertSettingUseCase
{
    private readonly IUnitOfWork _unitOfWork;

    public UpsertSettingUseCase(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<SettingDto>> ExecuteAsync(UpsertSettingRequest request, CancellationToken cancellationToken = default)
    {
        var existingSetting = await _unitOfWork.SettingRepository.GetSettingAsync(cancellationToken);

        Domain.Modules.Setting.Aggregates.Setting setting;

        if (existingSetting is null)
        {
            setting = Domain.Modules.Setting.Aggregates.Setting.Create(
                request.LessThanEnoughThreshold,
                request.MoreThanEnoughThreshold,
                request.DaysWithoutEatingForAlert,
                request.DaysWithoutEatingForWarning);

            await _unitOfWork.SettingRepository.CreateSettingAsync(setting, cancellationToken);
        }
        else
        {
            existingSetting.Update(
                request.LessThanEnoughThreshold,
                request.MoreThanEnoughThreshold,
                request.DaysWithoutEatingForAlert,
                request.DaysWithoutEatingForWarning);

            setting = existingSetting;
            _unitOfWork.SettingRepository.UpdateSettingAsync(setting, cancellationToken);
        }

        await _unitOfWork.CommitAsync(cancellationToken);

        return new SettingDto
        {
            Id = setting.Id.Value,
            LessThanEnoughThreshold = setting.LessThanEnoughThreshold,
            MoreThanEnoughThreshold = setting.MoreThanEnoughThreshold,
            DaysWithoutEatingForAlert = setting.DaysWithoutEatingForAlert,
            DaysWithoutEatingForWarning = setting.DaysWithoutEatingForWarning,
            CreatedAt = setting.CreatedAt,
            UpdatedAt = setting.UpdatedAt
        };
    }
}
