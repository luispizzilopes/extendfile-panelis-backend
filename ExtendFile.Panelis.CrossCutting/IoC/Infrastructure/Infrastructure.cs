using ExtendFile.Panelis.CrossCutting.IoC.Infrastructure.Caching;
using ExtendFile.Panelis.CrossCutting.IoC.Infrastructure.Clients;
using ExtendFile.Panelis.CrossCutting.IoC.Infrastructure.Context;
using ExtendFile.Panelis.CrossCutting.IoC.Infrastructure.Identity;
using ExtendFile.Panelis.CrossCutting.IoC.Infrastructure.Repositories;
using ExtendFile.Panelis.CrossCutting.IoC.Infrastructure.Services;
using ExtendFile.Panelis.CrossCutting.IoC.Infrastructure.UnitOfWork;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ExtendFile.Panelis.CrossCutting.IoC.Infrastructure;

public static class Infrastructure
{
    public static void AddInfrastructureDependencyInjection(
        this IServiceCollection services,
        IConfiguration configuration,
        IWebHostEnvironment environment)
    {
        services.AddAppDbContextDependencyInjection(configuration);
        services.AddRedisCache(configuration, environment);
        services.AddRepositoriesDependencyInjection();
        services.AddUnitOfWorkDependencyInjection();
        services.AddServicesDependencyInjection(configuration);
        services.AddIdentityDependencyInjection();
        services.AddFileListClient(configuration);
    }
}