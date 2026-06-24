using ExtendFile.Panelis.Application.Modules.House.UseCases.CreateBox;
using ExtendFile.Panelis.Application.Modules.House.UseCases.CreateHouse;
using ExtendFile.Panelis.Application.Modules.House.UseCases.DeleteBox;
using ExtendFile.Panelis.Application.Modules.House.UseCases.DeleteHouse;
using ExtendFile.Panelis.Application.Modules.House.UseCases.GetHouseById;
using ExtendFile.Panelis.Application.Modules.House.UseCases.GetHouses;
using ExtendFile.Panelis.Application.Modules.House.UseCases.UpdateBox;
using ExtendFile.Panelis.Application.Modules.House.UseCases.UpdateHouse;
using ExtendFile.Panelis.Application.Modules.Cat.UseCases.CreateCat;
using ExtendFile.Panelis.Application.Modules.Cat.UseCases.UpdateCat;
using ExtendFile.Panelis.Application.Modules.Cat.UseCases.DeleteCat;
using ExtendFile.Panelis.Application.Modules.Cat.UseCases.GetCatById;
using ExtendFile.Panelis.Application.Modules.Cat.UseCases.GetCats;
using ExtendFile.Panelis.Application.Modules.Cat.UseCases.GetCatsByBoxId;
using ExtendFile.Panelis.Application.Modules.Dashboard.UseCases.GetDashboard;
using ExtendFile.Panelis.Application.Modules.Dashboard.UseCases.GetCatsWithoutEating;
using ExtendFile.Panelis.Application.Modules.Setting.UseCases.GetSetting;
using ExtendFile.Panelis.Application.Modules.Setting.UseCases.UpsertSetting;
using ExtendFile.Panelis.Application.Modules.Test.UseCases.CreateTest;
using ExtendFile.Panelis.Application.Modules.Test.UseCases.DeleteTest;
using ExtendFile.Panelis.Application.Modules.Test.UseCases.GetCountDaysWithoutTest;
using ExtendFile.Panelis.Application.Modules.Test.UseCases.GetTestLinesByCatWithoutEating;
using ExtendFile.Panelis.Application.Modules.Test.UseCases.GetTestLinesByCatId;
using ExtendFile.Panelis.Application.Modules.Test.UseCases.ProcessTest;
using ExtendFile.Panelis.Application.Modules.Test.UseCases.GetTestsByBoxId;
using ExtendFile.Panelis.Application.Modules.Test.UseCases.GetTestLinesByTestId;
using ExtendFile.Panelis.Application.Modules.User.UseCases.Login;
using ExtendFile.Panelis.Application.Modules.User.UseCases.ResetPassword;
using ExtendFile.Panelis.Application.Modules.User.UseCases.UserList;
using ExtendFile.Panelis.Application.Modules.User.UseCases.UserUpdate;
using ExtendFile.Panelis.Application.Modules.User.UseCases.UserCreate;
using ExtendFile.Panelis.Application.Modules.User.UseCases.RequestPasswordReset;
using ExtendFile.Panelis.Application.Modules.User.UseCases.VerifyPasswordResetCode;
using ExtendFile.Panelis.Application.Modules.User.UseCases.ConfirmPasswordReset;
using ExtendFile.Panelis.Application.Modules.Report.UseCases.GetCatFoodConsumption;
using Microsoft.Extensions.DependencyInjection;

namespace ExtendFile.Panelis.CrossCutting.IoC.Application.UseCases;

public static class UseCasesDependencyInjection
{
    public static void AddUseCasesDependencyInjection(this IServiceCollection services)
    {
        services.AddScoped<GetHouseByIdUseCase>();
        services.AddScoped<GetHousesUseCase>();
        services.AddScoped<CreateHouseUseCase>();
        services.AddScoped<UpdateHouseUseCase>();
        services.AddScoped<DeleteHouseUseCase>();

        services.AddScoped<CreateBoxUseCase>();
        services.AddScoped<UpdateBoxUseCase>();
        services.AddScoped<DeleteBoxUseCase>();
        
        services.AddScoped<GetCatByIdUseCase>();
        services.AddScoped<GetCatsUseCase>();
        services.AddScoped<GetCatsByBoxIdUseCase>();
        services.AddScoped<CreateCatUseCase>();
        services.AddScoped<UpdateCatUseCase>();
        services.AddScoped<DeleteCatUseCase>();

        services.AddScoped<LoginUseCase>();
        services.AddScoped<UserListUseCase>();
        services.AddScoped<UserUpdateUseCase>();
        services.AddScoped<UserCreateUseCase>();
        services.AddScoped<ResetPasswordUseCase>();
        services.AddScoped<RequestPasswordResetUseCase>();
        services.AddScoped<VerifyPasswordResetCodeUseCase>();
        services.AddScoped<ConfirmPasswordResetUseCase>();

        services.AddScoped<GetDashboardUseCase>();
        services.AddScoped<GetCatsWithoutEatingUseCase>();

        services.AddScoped<CreateTestUseCase>();
        services.AddScoped<DeleteTestUseCase>();
        services.AddScoped<ProcessTestUseCase>();
        services.AddScoped<GetTestsByBoxIdUseCase>();
        services.AddScoped<GetTestLinesByTestIdUseCase>();
        services.AddScoped<GetCountDaysWithoutTestUseCase>();
        services.AddScoped<GetTestLinesByCatWithoutEatingUseCase>();
        services.AddScoped<GetTestLinesByCatIdUseCase>();

        services.AddScoped<GetSettingUseCase>();
        services.AddScoped<UpsertSettingUseCase>();

        services.AddScoped<GetCatFoodConsumptionUseCase>();
    }
}