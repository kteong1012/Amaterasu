using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine;

namespace YooAsset.Editor
{
    internal static class BuildLogger
    {
        private static bool _enableLog = true;
        private static string _prefix = "<color=#FFA500>[BuildLogger]</color>";

        public static void InitLogger(bool enableLog)
        {
            _enableLog = enableLog;
        }

        public static void Log(string message)
        {
            if (_enableLog)
            {
                Debug.Log(_prefix + message);
            }
        }
        public static void Warning(string message)
        {
            Debug.LogWarning(_prefix + message);
        }
        public static void Error(string message)
        {
            Debug.LogError(_prefix + message);
        }

        public static string GetErrorMessage(ErrorCode code, string message)
        {
            return $"[ErrorCode{(int)code}] {message}";
        }
    }
}