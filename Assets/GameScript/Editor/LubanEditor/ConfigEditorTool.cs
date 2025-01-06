using UnityEditor;
using SimpleJSON;
using GameEditor.ConfigEditor.Model;
using UnityEngine;
using System.Collections.Generic;

namespace GameEditor.ConfigEditor
{
    public class ConfigEditorTool : EditorWindow
    {

        [MenuItem("Tools/配置/配置编辑器")]
        public static void Open()
        {
            var window = GetWindow<ConfigEditorTool>();
            window.titleContent = new GUIContent("配置编辑器");
            window.Show();
        }

        private EditorConfig _editorConfig;
        public EditorConfig EditorConfig
        {
            get
            {
                if (_editorConfig == null)
                {
                    _editorConfig = new EditorConfig(GetTablePath);
                }
                return _editorConfig;
            }
        }

        private static string GetTablePath(string tableName)
        {
            var path = $"Config/Datas/DataJson/{tableName}.json";
            if(System.IO.File.Exists(path))
            {
                return path;
            }
            return string.Empty;
        }

        private void Export()
        {
            var workplaceDir = "Config";
            var batFileName = $"gen_client.bat";

            WindowsTools.RunBatch(batFileName, workplaceDir);
            AssetDatabase.Refresh();
        }

        private void Reload()
        {
            var workplaceDir = "Config";
            var batFileName = $"gen_editor.bat";

            WindowsTools.RunBatch(batFileName, workplaceDir);
            AssetDatabase.Refresh();
        }

        private void OnGUI()
        {
            DrawTopBar();
            EditorConfig.OnGUI();
        }

        private void DrawTopBar()
        {
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("导出所有表", GUILayout.Width(100)))
            {
                Export();
            }
            if (GUILayout.Button("重新加载", GUILayout.Width(100)))
            {
                Reload();
            }
            GUILayout.EndHorizontal();
        }
    }
}