using ExtendFile.Panelis.CrossCutting.IoC.Infrastructure.Context;
using ExtendFile.Panelis.CrossCutting.IoC.Infrastructure.Repositories;
using ExtendFile.Panelis.CrossCutting.IoC.Infrastructure.UnitOfWork;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ExtendFile.Panelis.CrossCutting.IoC.Infrastructure;

public static class Infrastructure
{
    public static void AddInfrastructureDependencyInjection(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAppDbContextDependencyInjection(configuration);
        services.AddRepositoriesDependencyInjection();
        services.AddUnitOfWorkDependencyInjection();
    }
}