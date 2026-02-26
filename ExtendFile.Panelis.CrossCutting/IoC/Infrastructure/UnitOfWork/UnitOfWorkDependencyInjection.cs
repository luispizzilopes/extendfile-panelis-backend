using ExtendFile.Panelis.Domain.Interfaces.UnitOfWork;
using Microsoft.Extensions.DependencyInjection;

namespace ExtendFile.Panelis.CrossCutting.IoC.Infrastructure.UnitOfWork;

public static class UnitOfWorkDependencyInjection
{
    public static void AddUnitOfWorkDependencyInjection(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, Panelis.Infrastructure.UnitOfWork.UnitOfWork>();
    }
}