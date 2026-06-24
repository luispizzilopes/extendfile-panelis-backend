using ExtendFile.Panelis.Application.Modules.Dashboard.Interfaces.Repositories;
using ExtendFile.Panelis.Application.Modules.Report.Interfaces.Repositories;
using ExtendFile.Panelis.Domain.Interfaces.Repositories;
using ExtendFile.Panelis.Domain.Interfaces.Repositories.Cat;
using ExtendFile.Panelis.Domain.Interfaces.Repositories.Setting;
using ExtendFile.Panelis.Domain.Interfaces.Repositories.Test;
using ExtendFile.Panelis.Infrastructure.Repositories.Cat;
using ExtendFile.Panelis.Infrastructure.Repositories.Dashboard;
using ExtendFile.Panelis.Infrastructure.Repositories.House;
using ExtendFile.Panelis.Infrastructure.Repositories.Report;
using ExtendFile.Panelis.Infrastructure.Repositories.Setting;
using ExtendFile.Panelis.Infrastructure.Repositories.Test;
using Microsoft.Extensions.DependencyInjection;

namespace ExtendFile.Panelis.CrossCutting.IoC.Infrastructure.Repositories;

public static class RepositoriesDependencyInjection
{
    public static void AddRepositoriesDependencyInjection(this IServiceCollection services)
    {
        services.AddScoped<IHouseRepository, HouseRepository>();
        services.AddScoped<ICatRepository, CatRepository>();
        services.AddScoped<ITestRepository, TestRepository>();
        services.AddScoped<ISettingRepository, SettingRepository>();

        services.AddScoped<IDashboardReadModelRepository, DashboardReadModelRepository>();
        services.AddScoped<IReportRepository, ReportRepository>();
    }
}