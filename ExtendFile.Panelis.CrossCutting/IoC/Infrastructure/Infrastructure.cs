using ExtendFile.Panelis.CrossCutting.IoC.Infrastructure.Context;
using ExtendFile.Panelis.CrossCutting.IoC.Infrastructure.Identity;
using ExtendFile.Panelis.CrossCutting.IoC.Infrastructure.Repositories;
using ExtendFile.Panelis.CrossCutting.IoC.Infrastructure.Services;
using ExtendFile.Panelis.CrossCutting.IoC.Infrastructure.Token;
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
        services.AddServicesDependencyInjection();
        services.AddTokenDependencyInjection(configuration);
        services.AddIdentityDependencyInjection();
    }
}