using System;
using UnityEngine;

public class UnityConsoleLog : ILogWithObject
{
    public Color DeubgColor = Color.white;
    public Color InfoColor = Color.green;
    public Color WarningColor = Color.yellow;
    public Color ErrorColor = Color.red;
    public Color ExceptionColor = Color.red;

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
        return $"<color=#{ColorUtility.ToHtmlStringRGB(color)}>{str}</color>";
#else
        return str;
#endif
    }
}
