using ExtendFile.Panelis.Domain.Interfaces.Logger;
using ExtendFile.Panelis.Infrastructure.Logger;
using Serilog;
using Serilog.Events;

namespace ExtendFile.Panelis.Presentation.Extensions;

public static class LoggerExtension
{
    public static IServiceCollection AddCustomLogger(this IServiceCollection services, IConfiguration configuration)
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Warning)
            .Filter.ByExcluding(logEvent =>
                logEvent.Properties.ContainsKey("SourceContext") &&
                logEvent.Properties["SourceContext"].ToString().Contains("Microsoft.EntityFrameworkCore") &&
                logEvent.Level == LogEventLevel.Warning)
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .WriteTo.File("Logs/log.txt", rollingInterval: RollingInterval.Day)
            .CreateLogger();

        services.AddSingleton<Serilog.ILogger>(Log.Logger); 
        services.AddSingleton<ILoggerManager, LoggerManager>();

        return services;
    }
}