using UnityEngine;

namespace Game.Log
{
    public interface IGameLog
    {
        void Log(LogLevel level, string message);
        void Exception(LogLevel level,  System.Exception exception);
    }

    public interface IGameLogWithObject : IGameLog
    {
        void Log(LogLevel level, string message, Object @object);
        void Exception(LogLevel level, System.Exception exception, Object @object);
    }
}