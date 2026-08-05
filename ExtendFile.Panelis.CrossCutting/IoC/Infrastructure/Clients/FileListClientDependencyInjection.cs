using ExtendFile.Panelis.Application.Interfaces.Clients;
using ExtendFile.Panelis.Infrastructure.Clients;
using ExtendFile.Panelis.Infrastructure.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ExtendFile.Panelis.CrossCutting.IoC.Infrastructure.Clients;

public static class FileListClientDependencyInjection
{
    public static void AddFileListClient(this IServiceCollection services, IConfiguration configuration)
    {
        var baseUrl = Resolve(configuration, "FileListService:BaseUrl", "FILELIST_BASE_URL");
        var apiKey = Resolve(configuration, "FileListService:ApiKey", "FILELIST_API_KEY");

        var settings = new FileListSettings { BaseUrl = baseUrl, ApiKey = apiKey };

        services.Configure<FileListSettings>(options =>
        {
            options.BaseUrl = settings.BaseUrl;
            options.ApiKey = settings.ApiKey;
        });

        services.AddHttpClient<IFileListClient, FileListClient>(client =>
        {
            client.BaseAddress = new Uri(baseUrl);
            client.DefaultRequestHeaders.Add("X-Integration-Key", apiKey);
        });
    }

    private static string Resolve(IConfiguration configuration, string configKey, string envVar)
    {
        var value = configuration[configKey];
        if (string.IsNullOrEmpty(value))
            value = Environment.GetEnvironmentVariable(envVar);
        if (string.IsNullOrEmpty(value))
            throw new InvalidOperationException($"Configuração '{configKey}' é obrigatória.");
        return value;
    }
}
