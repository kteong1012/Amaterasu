using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using UnityEngine;

public class Log
{
    private static ConcurrentDictionary<Type, ILog> _loggers = new ConcurrentDictionary<Type, ILog>();

    public static void RegisterLogger<T>(T logger) where T : ILog
    {
        _loggers.TryAdd(typeof(T), logger);
    }

    public static void RemoveLogger<T>() where T : ILog
    {
        _loggers.TryRemove(typeof(T), out _);
    }


#if !UNITY_EDITOR
    [Conditional("LOG_DEBUG")]
#endif
    public static void Debug(object message, UnityEngine.Object @object = null)
    {
        foreach (var (_, logger) in _loggers)
        {
            if (@object && logger is ILogWithObject objectLogger)
            {
                objectLogger.Log(LogLevel.Debug, message.ToString(), @object);
            }
            else
            {
                logger.Log(LogLevel.Debug, message.ToString());

            }
        }
    }

#if !UNITY_EDITOR
    [Conditional("LOG_INFO")]
#endif
    public static void Info(object message, UnityEngine.Object @object = null)
    {
        foreach (var (_, logger) in _loggers)
        {
            if (@object && logger is ILogWithObject objectLogger)
            {
                objectLogger.Log(LogLevel.Info, message.ToString(), @object);
            }
            else
            {
                logger.Log(LogLevel.Info, message.ToString());
            }
        }
    }

#if !UNITY_EDITOR
    [Conditional("LOG_WARNING")]
#endif
    public static void Warning(object message, UnityEngine.Object @object = null)
    {
        foreach (var (_, logger) in _loggers)
        {
            if (@object && logger is ILogWithObject objectLogger)
            {
                objectLogger.Log(LogLevel.Warning, message.ToString(), @object);
            }
            else
            {
                logger.Log(LogLevel.Warning, message.ToString());
            }
        }
    }

    public static void Error(object message, UnityEngine.Object @object = null)
    {
        foreach (var (_, logger) in _loggers)
        {
            if (@object && logger is ILogWithObject objectLogger)
            {
                objectLogger.Log(LogLevel.Error, message.ToString(), @object);
            }
            else
            {
                logger.Log(LogLevel.Error, message.ToString());
            }
        }
    }

    public static void Exception(Exception exception, UnityEngine.Object @object = null)
    {
        foreach (var (_, logger) in _loggers)
        {
            if (@object && logger is ILogWithObject objectLogger)
            {
                objectLogger.Exception(LogLevel.Error, exception, @object);
            }
            else
            {
                logger.Exception(LogLevel.Error, exception);
            }
        }
    }
}
