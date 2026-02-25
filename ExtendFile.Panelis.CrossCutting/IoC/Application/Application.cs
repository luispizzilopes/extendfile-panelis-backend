using ExtendFile.Panelis.Application.Behavior;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ExtendFile.Panelis.CrossCutting.IoC.Application;

public static class Application
{
    public static void AddApplicationDependencyInjection(this IServiceCollection services, IConfiguration configuration)
    {
        var assemblies = AppDomain.CurrentDomain.GetAssemblies();
        
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(assemblies));
    }
}