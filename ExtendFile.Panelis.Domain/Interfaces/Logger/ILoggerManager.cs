namespace ExtendFile.Panelis.Domain.Interfaces.Logger;

public interface ILoggerManager
{
    void LogInfo(string message);
    void LogInfo(string messageTemplate, params object[] args);

    void LogWarn(string message);
    void LogWarn(string messageTemplate, params object[] args);
    void LogWarn(Exception exception, string message);
    void LogWarn(Exception exception, string messageTemplate, params object[] args);

    void LogDebug(string message);
    void LogDebug(string messageTemplate, params object[] args);

    void LogError(string message);
    void LogError(Exception exception, string message);
    void LogError(string messageTemplate, params object[] args);
    void LogError(Exception exception, string messageTemplate, params object[] args);

    void LogCritical(string message);
    void LogCritical(Exception exception, string message);
    void LogCritical(string messageTemplate, params object[] args);
    void LogCritical(Exception exception, string messageTemplate, params object[] args);

    bool IsDebugEnabled { get; }
    bool IsInfoEnabled { get; }
    bool IsWarnEnabled { get; }
    bool IsErrorEnabled { get; }

    ILoggerManager ForContext<T>();
}