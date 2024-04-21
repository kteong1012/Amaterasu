using Game;
using HybridCLR.Editor;
using HybridCLR.Editor.Commands;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace GameEditor
{
    public static class BuildTools
    {
        [MenuItem("BuildTools/CompileDllAndCopyToGameRes")]
        public static void CompileDllAndCopyToGameRes()
        {
            var target = EditorUserBuildSettings.activeBuildTarget;
            CompileDllCommand.CompileDll(target);

            var hotUpdateDllDir = SettingsUtil.GetHotUpdateDllsOutputDirByTarget(target);
            var srcPath = $"{hotUpdateDllDir}/{AppSettings.HotUpdateDllName}";
            var destPath = AppSettings.HotUpdateDllAssetPath;
            FileUtil.ReplaceFile(srcPath, destPath);
            AssetDatabase.Refresh();
            Debug.Log("CompileDllAndCopyToGameRes Done!");
        }

        [MenuItem("BuildTools/CopyAotDllsToResources")]
        public static void CopyAotDllsToResources()
        {
            var target = EditorUserBuildSettings.activeBuildTarget;
            var aotDllDir = SettingsUtil.AssembliesPostIl2CppStripDir;
            var outputDir = "Assets/Resources/AotDlls";
            if (Directory.Exists(outputDir))
            {
                Directory.Delete(outputDir, true);
            }
            Directory.CreateDirectory(outputDir);

            var aotAssNames = AOTGenericReferences.PatchedAOTAssemblyList;
            foreach (var assName in aotAssNames)
            {
                var srcPath = $"{aotDllDir}/{target}/{assName}";
                var destPath = $"{outputDir}/{assName}.bytes";
                FileUtil.ReplaceFile(srcPath, destPath);
            }
            AssetDatabase.Refresh();
            Debug.Log("CopyAotDllsToResources Done!");
        }
    }
}
