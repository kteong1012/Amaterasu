using System;
using UnityEngine;
using YooAsset;

namespace Game.Log
{
    public class UnityConsoleLog : IGameLogWithObject, YooAsset.ILogger
    {
        private static UnityConsoleLog _instance;
        public static UnityConsoleLog Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new UnityConsoleLog();
                }
                return _instance;
            }
        }

        public Color DeubgColor = new Color(1, 0.5f, 0);
        public Color InfoColor = Color.cyan;
        public Color WarningColor = Color.yellow;
        public Color ErrorColor = Color.clear;
        public Color ExceptionColor = Color.clear;

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
                    Debug.Log(GetColorString(message, DeubgColor), @object);
                    break;
                case LogLevel.Info:
                    Debug.Log(GetColorString(message, InfoColor), @object);
                    break;
                case LogLevel.Warning:
                    Debug.LogWarning(GetColorString(message, WarningColor), @object);
                    break;
                case LogLevel.Error:
                    Debug.LogError(GetColorString(message, ErrorColor), @object);
                    break;
            }
        }

        public void Log(LogLevel level, string message)
        {
            switch (level)
            {
                case LogLevel.Debug:
                    Debug.Log(GetColorString(message, DeubgColor));
                    break;
                case LogLevel.Info:
                    Debug.Log(GetColorString(message, InfoColor));
                    break;
                case LogLevel.Warning:
                    Debug.LogWarning(GetColorString(message, WarningColor));
                    break;
                case LogLevel.Error:
                    Debug.LogError(GetColorString(message, ErrorColor));
                    break;
            }
        }

        private static string GetColorString(string str, Color color)
        {
#if UNITY_EDITOR
            var lines = str.Split('\n');
            for (int i = 0; i < lines.Length; i++)
            {
                lines[i] = $"<color=#{ColorUtility.ToHtmlStringRGB(color)}>{lines[i]}</color>";
            }
            return string.Join("\n", lines);
#else
            return str;
#endif
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
