using ExtendFile.Panelis.Domain.Interfaces.Logger;
using Serilog;
using Serilog.Events;

namespace ExtendFile.Panelis.Infrastructure.Logger;

public class LoggerManager : ILoggerManager
{
    private readonly ILogger _logger;

    public LoggerManager(ILogger logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    private LoggerManager(ILogger logger, bool isContextual)
    {
        _logger = logger;
    }

    public bool IsDebugEnabled => _logger.IsEnabled(LogEventLevel.Debug);
    public bool IsInfoEnabled => _logger.IsEnabled(LogEventLevel.Information);
    public bool IsWarnEnabled => _logger.IsEnabled(LogEventLevel.Warning);
    public bool IsErrorEnabled => _logger.IsEnabled(LogEventLevel.Error);

    public void LogInfo(string message)
    {
        if (IsInfoEnabled)
            _logger.Information(message);
    }

    public void LogInfo(string messageTemplate, params object[] args)
    {
        if (IsInfoEnabled)
            _logger.Information(messageTemplate, args);
    }

    public void LogWarn(string message)
    {
        if (IsWarnEnabled)
            _logger.Warning(message);
    }

    public void LogWarn(string messageTemplate, params object[] args)
    {
        if (IsWarnEnabled)
            _logger.Warning(messageTemplate, args);
    }

    public void LogWarn(Exception exception, string message)
    {
        if (IsWarnEnabled)
            _logger.Warning(exception, message);
    }

    public void LogWarn(Exception exception, string messageTemplate, params object[] args)
    {
        if (IsWarnEnabled)
            _logger.Warning(exception, messageTemplate, args);
    }

    public void LogDebug(string message)
    {
        if (IsDebugEnabled)
            _logger.Debug(message);
    }

    public void LogDebug(string messageTemplate, params object[] args)
    {
        if (IsDebugEnabled)
            _logger.Debug(messageTemplate, args);
    }

    public void LogError(string message)
    {
        if (IsErrorEnabled)
            _logger.Error(message);
    }

    public void LogError(Exception exception, string message)
    {
        if (IsErrorEnabled)
            _logger.Error(exception, message);
    }

    public void LogError(string messageTemplate, params object[] args)
    {
        if (IsErrorEnabled)
            _logger.Error(messageTemplate, args);
    }

    public void LogError(Exception exception, string messageTemplate, params object[] args)
    {
        if (IsErrorEnabled)
            _logger.Error(exception, messageTemplate, args);
    }

    public void LogCritical(string message)
    {
        _logger.Fatal(message);
    }

    public void LogCritical(Exception exception, string message)
    {
        _logger.Fatal(exception, message);
    }

    public void LogCritical(string messageTemplate, params object[] args)
    {
        _logger.Fatal(messageTemplate, args);
    }

    public void LogCritical(Exception exception, string messageTemplate, params object[] args)
    {
        _logger.Fatal(exception, messageTemplate, args);
    }

    public ILoggerManager ForContext<T>()
    {
        var typeName = typeof(T).Name;
        var propertyName = nameof(T);
        var contextualLogger = _logger
            .ForContext("ContextInfo", $"{typeName}.{propertyName}");

        return new LoggerManager(contextualLogger, true);
    }
}