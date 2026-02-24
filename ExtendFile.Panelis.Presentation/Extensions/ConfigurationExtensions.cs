namespace ExtendFile.Panelis.Presentation.Extensions;

public static class ConfigurationExtensions
{
    public static void AddConfigurations(
        this IServiceCollection services,
        ConfigurationManager configurationManager,
        IWebHostEnvironment hostingEnvironment)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
    }
}