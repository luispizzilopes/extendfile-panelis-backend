using ExtendFile.Panelis.Domain.Interfaces.Repositories;
using ExtendFile.Panelis.Domain.Interfaces.Repositories.Cat;
using ExtendFile.Panelis.Infrastructure.Repositories.Cat;
using ExtendFile.Panelis.Infrastructure.Repositories.House;
using Microsoft.Extensions.DependencyInjection;

namespace ExtendFile.Panelis.CrossCutting.IoC.Infrastructure.Repositories;

public static class RepositoriesDependencyInjection
{
    public static void AddRepositoriesDependencyInjection(this IServiceCollection services)
    {
        services.AddScoped<IHouseRepository, HouseRepository>();
        services.AddScoped<ICatRepository, CatRepository>();
    }
}