using ExtendFile.Panelis.Application.Modules.House.UseCases.CreateBox;
using ExtendFile.Panelis.Application.Modules.House.UseCases.CreateHouse;
using ExtendFile.Panelis.Application.Modules.House.UseCases.DeleteBox;
using ExtendFile.Panelis.Application.Modules.House.UseCases.DeleteHouse;
using ExtendFile.Panelis.Application.Modules.House.UseCases.GetAllHouses;
using ExtendFile.Panelis.Application.Modules.House.UseCases.GetHouseById;
using ExtendFile.Panelis.Application.Modules.House.UseCases.GetHouses;
using ExtendFile.Panelis.Application.Modules.House.UseCases.UpdateBox;
using ExtendFile.Panelis.Application.Modules.House.UseCases.UpdateHouse;
using Microsoft.Extensions.DependencyInjection;

namespace ExtendFile.Panelis.CrossCutting.IoC.Application.UseCases;

public static class UseCasesDependencyInjection
{
    public static void AddUseCasesDependencyInjection(this IServiceCollection services)
    {
        services.AddScoped<GetAllHousesUseCase>();
        services.AddScoped<GetHouseByIdUseCase>();
        services.AddScoped<GetHousesUseCase>();
        services.AddScoped<CreateHouseUseCase>();
        services.AddScoped<UpdateHouseUseCase>();
        services.AddScoped<DeleteHouseUseCase>();

        services.AddScoped<CreateBoxUseCase>();
        services.AddScoped<UpdateBoxUseCase>();
        services.AddScoped<DeleteBoxUseCase>();
    }
}