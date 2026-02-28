using ExtendFile.Panelis.CrossCutting.IoC.Application;
using ExtendFile.Panelis.CrossCutting.IoC.Infrastructure;
using ExtendFile.Panelis.Presentation.Swagger;

namespace ExtendFile.Panelis.Presentation.Extensions;

public static class ConfigurationExtension
{
    public static void AddConfigurations(
        this IServiceCollection services,
        ConfigurationManager configurationManager,
        IWebHostEnvironment hostingEnvironment)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddSwaggerConfiguration(configurationManager);
        
        services.AddInfrastructureDependencyInjection(configurationManager);
        services.AddApplicationDependencyInjection(configurationManager);
        
        services.AddCustomLogger(configurationManager);

        services.AddCors(); 
    }
}