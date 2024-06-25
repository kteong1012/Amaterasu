using UnityEngine;

namespace Game.Log
{
    public interface IGameLogger
    {
        void Log(LogLevel level, string message);
        void Exception(LogLevel level,  System.Exception exception);
    }

    public interface IGameLoggerWithObject : IGameLogger
    {
        void Log(LogLevel level, string message, Object @object);
        void Exception(LogLevel level, System.Exception exception, Object @object);
    }
}