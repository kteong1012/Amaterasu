using Game;
using HybridCLR.Editor;
using HybridCLR.Editor.Commands;
using HybridCLR.Editor.HotUpdate;
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


            var aotDir = $"{SettingsUtil.AssembliesPostIl2CppStripDir}/{target}";
            var checker = new MissingMetadataChecker(aotDir, new string[] { });
            checker.Check(srcPath);
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

#if UNITY_ANDROID
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
#endif
    }
}
