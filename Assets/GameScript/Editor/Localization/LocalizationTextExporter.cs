using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace GameEditor
{
    internal class LocalizationTextExporter
    {
        private const string _exportDirPath = "Config\\Localization\\Source";

        [MenuItem("Tools/多语言/导出Prefab中的文本")]
        public static void ExportPrefabLocalizationText()
        {
            var folder = "Assets/GameRes/Prefabs";

            var prefabs = AssetDatabase.FindAssets("t:Prefab", new[] { folder });
            var total = prefabs.Length;
            var current = 0;

            var keys = new HashSet<string>();
            foreach (var guid in prefabs)
            {
                if (EditorUtility.DisplayCancelableProgressBar("导出Prefab中的文本", $"正在导出Prefab中的文本...{current}/{total}", (float)current / total))
                {
                    return;
                }
                var path = AssetDatabase.GUIDToAssetPath(guid);
                var go = AssetDatabase.LoadAssetAtPath<UnityEngine.GameObject>(path);
                var comps = go.GetComponentsInChildren<Game.Localization>(true);
                foreach (var comp in comps)
                {
                    keys.Add(comp.key);
                }
            }
            EditorUtility.ClearProgressBar();

            var sb = new StringBuilder();
            foreach (var key in keys)
            {
                sb.AppendLine(key);
            }

            var fileName = "prefab.txt";
            var filePath = $"{_exportDirPath}/{fileName}";

            if (!Directory.Exists(_exportDirPath))
            {
                Directory.CreateDirectory(_exportDirPath);
            }

            File.WriteAllText(filePath, sb.ToString());

            Debug.Log($"导出Prefab中的文本成功，文件路径：{filePath}");
        }

        [MenuItem("Tools/多语言/导出代码中的文本")]
        public static void ExportCodeLocalizationText()
        {
            var workingDir = "Config/Localization";
            var batPath = $"{workingDir}/_CollectCode.bat";
            var batFi = new FileInfo(batPath);
            var workingDirFi = new DirectoryInfo(workingDir);
            using (var process = new System.Diagnostics.Process())
            {
                process.StartInfo.FileName = batFi.FullName;
                process.StartInfo.WorkingDirectory = workingDirFi.FullName;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.CreateNoWindow = true;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.RedirectStandardError = true;
                process.StartInfo.StandardOutputEncoding = Encoding.UTF8;
                process.StartInfo.StandardErrorEncoding = Encoding.UTF8;
                process.Start();
                process.WaitForExit();

                // 打印
                var output = process.StandardOutput.ReadToEnd();
                var error = process.StandardError.ReadToEnd();
                if (!string.IsNullOrEmpty(output))
                {
                    LogLines(output);
                }
                if (!string.IsNullOrEmpty(error))
                {
                    LogLines(error);
                }

            }

            void LogLines(string str)
            {
                var lines = str.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var line in lines)
                {
                    Debug.Log(line);
                }
            }
        }

        [MenuItem("Tools/多语言/导出预制体和代码文本")]
        public static void ExportAllLocalizationText()
        {
            ExportPrefabLocalizationText();
            ExportCodeLocalizationText();
        }
    }
}
