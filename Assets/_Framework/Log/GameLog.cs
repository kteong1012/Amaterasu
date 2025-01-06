using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

namespace Game.Log
{
    public class GameLog
    {
        public static LogLevel LogLevel { get; set; } = LogLevel.Debug;
        private static ConcurrentDictionary<Type, IGameLogger> _loggers = new ConcurrentDictionary<Type, IGameLogger>();

        public static void RegisterLogger<T>(T logger) where T : IGameLogger
        {
            _loggers.TryAdd(typeof(T), logger);
        }

        public static void RemoveLogger<T>() where T : IGameLogger
        {
            _loggers.TryRemove(typeof(T), out _);
        }

        public static void ClearLogger()
        {
            _loggers.Clear();
        }


        [Conditional("LOG_DEBUG")]
        [Conditional("UNITY_EDITOR")]
        public static void Debug(object message, UnityEngine.Object @object = null)
        {
            if(LogLevel < LogLevel.Debug)
            {
                return;
            }
            foreach (var (_, logger) in _loggers)
            {
                if (@object && logger is IGameLoggerWithObject objectLogger)
                {
                    objectLogger.Log(LogLevel.Debug, message.ToString(), @object);
                }
                else
                {
                    logger.Log(LogLevel.Debug, message.ToString());

                }
            }
        }

        public static void Info(object message, UnityEngine.Object @object = null)
        {
            if (LogLevel < LogLevel.Info)
            {
                return;
            }
            foreach (var (_, logger) in _loggers)
            {
                if (@object && logger is IGameLoggerWithObject objectLogger)
                {
                    objectLogger.Log(LogLevel.Info, message.ToString(), @object);
                }
                else
                {
                    logger.Log(LogLevel.Info, message.ToString());
                }
            }
        }

        public static void Warning(object message, UnityEngine.Object @object = null)
        {
            if (LogLevel < LogLevel.Warning)
            {
                return;
            }
            foreach (var (_, logger) in _loggers)
            {
                if (@object && logger is IGameLoggerWithObject objectLogger)
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
            if (LogLevel < LogLevel.Error)
            {
                return;
            }
            foreach (var (_, logger) in _loggers)
            {
                if (@object && logger is IGameLoggerWithObject objectLogger)
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
            if (LogLevel < LogLevel.Exception)
            {
                return;
            }
            foreach (var (_, logger) in _loggers)
            {
                if (@object && logger is IGameLoggerWithObject objectLogger)
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
}
