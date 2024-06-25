using System;
using UnityEngine;
using YooAsset;

namespace Game.Log
{
    public class UnityConsoleLogger : IGameLoggerWithObject, YooAsset.ILogger
    {
        private static UnityConsoleLogger _instance;
        public static UnityConsoleLogger Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new UnityConsoleLogger();
                }
                return _instance;
            }
        }

        public Color DeubgColor = new Color(1, 0.5f, 0);
        public Color InfoColor = Color.cyan;
        public Color WarningColor = Color.yellow;
        public Color ErrorColor = Color.white;
        public Color ExceptionColor = Color.white;

        public void Exception(LogLevel level, Exception exception, UnityEngine.Object @object)
        {
            Debug.LogException(exception, @object);
        }

        public void Exception(LogLevel level, Exception exception)
        {
            Debug.LogException(exception);
        }

        public void Log(LogLevel level, string message, UnityEngine.Object @object)
        {
            switch (level)
            {
                case LogLevel.Debug:
                    GetColorString(ref message, DeubgColor);
                    Debug.Log(message, @object);
                    break;
                case LogLevel.Info:
                    GetColorString(ref message, InfoColor);
                    Debug.Log(message, @object);
                    break;
                case LogLevel.Warning:
                    GetColorString(ref message, WarningColor);
                    Debug.LogWarning(message, @object);
                    break;
                case LogLevel.Error:
                    GetColorString(ref message, ErrorColor);
                    Debug.LogError(message, @object);
                    break;
            }
        }

        public void Log(LogLevel level, string message)
        {
            switch (level)
            {
                case LogLevel.Debug:
                    GetColorString(ref message, DeubgColor);
                    Debug.Log(message);
                    break;
                case LogLevel.Info:
                    GetColorString(ref message, InfoColor);
                    Debug.Log(message);
                    break;
                case LogLevel.Warning:
                    GetColorString(ref message, WarningColor);
                    Debug.LogWarning(message);
                    break;
                case LogLevel.Error:
                    GetColorString(ref message, ErrorColor);
                    Debug.LogError(message);
                    break;
            }
        }

        [System.Diagnostics.Conditional("UNITY_EDITOR")]
        private static void GetColorString(ref string str, Color color)
        {
            str = $"<color=#{ColorUtility.ToHtmlStringRGB(color)}>{str}</color>";
        }

        public void Log(string message)
        {
            Log(LogLevel.Debug, message);
        }

        public void Warning(string message)
        {
            Log(LogLevel.Warning, message);
        }

        public void Error(string message)
        {
            Log(LogLevel.Error, message);
        }

        public void Exception(Exception exception)
        {
            Debug.LogException(exception);
        }
    }
}
