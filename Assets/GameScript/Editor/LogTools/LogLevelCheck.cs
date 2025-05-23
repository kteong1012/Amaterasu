//using UnityEditor;
//using UnityEngine;
//using UnityEditor.PackageManager;
//using System.Linq;
//using UnityEditor.Build.Content;

//namespace GameEditor
//{
//    public class LogLevelCheck : Editor
//    {
//        // 定义需要检查的宏定义
//        private static readonly string[] _symbols = { "LOG_DEBUG", "LOG_INFO", "LOG_WARNING" };

//        private void Awake()
//        {
//            CheckSymbolState();
//        }

//        [MenuItem("Tools/LogLevel/Debug")]
//        private static void SetDebugSymbol()
//        {
//            SetSymbolState("LOG_DEBUG");
//        }

//        private static void CheckSymbolState()
//        {
//            foreach (string symbol in _symbols)
//            {
//                bool isEnabled = IsSymbolEnabled(symbol);
//                Menu.SetChecked("LogLevel/" + GetSymbolName(symbol), isEnabled);
//            }
//        }

//        // 设置宏定义的状态
//        private static void SetSymbolState(string symbol)
//        {
//            var target = EditorUserBuildSettings.activeBuildTarget;
//            var group = AndroidPlayerAndAssetsBuildPipeline.GetBuildTargetGroup(target);

//            var defines = PlayerSettings.GetScriptingDefineSymbolsForGroup(group);
//            var _symbols = defines.Split(';').Where(x => !string.IsNullOrWhiteSpace(x)).Distinct().ToList();
//            var contains = _symbols.Contains(symbol);
//            if (contains)
//            {
//                _symbols.Remove(symbol);
//            }
//            else
//            {
//                _symbols.Add(symbol);
//            }
//            defines = string.Join(";", _symbols.ToArray());
//            PlayerSettings.SetScriptingDefineSymbolsForGroup(group, defines);
//            CheckSymbolState();
//        }

//        // 检查宏定义是否启用
//        private static bool IsSymbolEnabled(string symbol)
//        {
//            var target = EditorUserBuildSettings.activeBuildTarget;
//            var group = AndroidPlayerAndAssetsBuildPipeline.GetBuildTargetGroup(target);
//            string defines = PlayerSettings.GetScriptingDefineSymbolsForGroup(group);
//            return defines.Contains(symbol);
//        }

//        // 获取宏定义的名称
//        private static string GetSymbolName(string symbol)
//        {
//            var shortName = symbol.Replace("LOG_", "");
//            var name = char.ToUpper(shortName[0]) + shortName.Substring(1).ToLower();
//            return name;
//        }
//    }
//}