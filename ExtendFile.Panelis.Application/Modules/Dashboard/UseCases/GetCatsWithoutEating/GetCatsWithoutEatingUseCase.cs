using ErrorOr;
using ExtendFile.Panelis.Application.Modules.Dashboard.Requests.GetCatsWithoutEating;
using ExtendFile.Panelis.Application.Modules.Dashboard.Responses.GetCatsWithoutEating;
using ExtendFile.Panelis.Domain.Interfaces.UnitOfWork;

namespace ExtendFile.Panelis.Application.Modules.Dashboard.UseCases.GetCatsWithoutEating;

public class GetCatsWithoutEatingUseCase
{
    private readonly IUnitOfWork _unitOfWork;

    public GetCatsWithoutEatingUseCase(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<GetCatsWithoutEatingResponse>> ExecuteAsync(
        GetCatsWithoutEatingRequest request, 
        CancellationToken cancellationToken = default)
    {
        var settings = await _unitOfWork.SettingRepository.GetSettingAsync(cancellationToken);
        if (settings is null) return new GetCatsWithoutEatingResponse();

        var cats = await _unitOfWork.CatRepository.GetCatsWithoutEatingAsync(
            settings.DaysWithoutEatingForAlert, 
            settings.DaysWithoutEatingForWarning, 
            cancellationToken);
        
        var catsWithoutEating = new List<CatWithoutEatingDto>();

        foreach (var cat in cats)
        {
            var status = GetEatingStatus(cat.DaysWithoutEating, settings.DaysWithoutEatingForAlert, settings.DaysWithoutEatingForWarning);
            
            if (status is not null)
            {
                var house = await _unitOfWork.HouseRepository.GetHouseByBoxIdAsync(cat.BoxId.Value, cancellationToken);
                var box = house?.Boxes.FirstOrDefault(b => b.Id == cat.BoxId);

                catsWithoutEating.Add(new CatWithoutEatingDto
                {
                    CatId = cat.Id.Value,
                    CatName = cat.Name,
                    HouseName = house?.Name ?? string.Empty,
                    BoxName = box?.Name ?? string.Empty,
                    DaysWithoutEating = cat.DaysWithoutEating,
                    Status = status
                });
            }
        }

        return new GetCatsWithoutEatingResponse
        {
            Cats = catsWithoutEating.OrderBy(c => c.Status).ThenBy(c => c.DaysWithoutEating).ToList()
        };
    }

    private static string? GetEatingStatus(int daysWithoutEating, int alertDays, int warningDays)
    {
        if (daysWithoutEating >= warningDays)
            return "Warning";
        
        if (daysWithoutEating >= alertDays)
            return "Alert";

        return null;
    }
}
