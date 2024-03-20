using UnityEngine;

public interface ILog
{
    void Log(LogLevel level, string message);
    void Exception(LogLevel level,  System.Exception exception);
}

public interface ILogWithObject : ILog
{
    void Log(LogLevel level, string message, Object @object);
    void Exception(LogLevel level, System.Exception exception, Object @object);
}