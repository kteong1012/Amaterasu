using Game;
using HybridCLR.Editor;
using HybridCLR.Editor.Commands;
using HybridCLR.Editor.HotUpdate;
using HybridCLR.Editor.Settings;
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

            foreach (var dllName in GameConfig.Instance.HotupdateConfig.HotupdateAssemblies)
            {
                var fileName = $"{dllName}.dll";
                var srcPath = $"{hotUpdateDllDir}/{fileName}";
                var destPath = Path.Combine(GameConfig.Instance.HotupdateConfig.HotUpdateDllAssetFolder, fileName);
                FileUtil.ReplaceFile(srcPath, destPath);
            }

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

        [MenuItem("BuildTools/BuildPlayer")]
        public static void BuildAndroid()
        {
            var options = BuildOptions.None;
            BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions
            {
                locationPathName = "Builds/base.apk",
                options = options
            };
            buildPlayerOptions.scenes = new[] { "Assets/Scenes/Launch.unity" };
            buildPlayerOptions.target = BuildTarget.Android;
            BuildPipeline.BuildPlayer(buildPlayerOptions);
        }
    }
}
