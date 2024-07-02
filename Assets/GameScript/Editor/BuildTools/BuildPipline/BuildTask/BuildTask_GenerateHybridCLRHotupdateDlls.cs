using HybridCLR.Editor;
using HybridCLR.Editor.Commands;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace GameEditor
{
    [BuildTask(BuildParamGroup.Assets)]
    public class BuildTask_GenerateHybridCLRHotupdateDlls : IBuildTask
    {
        public void Run(BuildContext context)
        {
            CompileDllAndCopyToGameRes(context);;

            AssetDatabase.Refresh();
        }
        public static void CompileDllAndCopyToGameRes(BuildContext contenxt)
        {
            var target = EditorUserBuildSettings.activeBuildTarget;
            CompileDllCommand.CompileDll(target);

            var hotUpdateDllDir = SettingsUtil.GetHotUpdateDllsOutputDirByTarget(target);
            var outputDir = "Assets/GameRes/Dlls";

            foreach (var dllName in contenxt.BuildParameters.hotUpdateAssemblies)
            {
                var fileName = $"{dllName}.dll";
                var srcPath = $"{hotUpdateDllDir}/{fileName}";
                var destPath = Path.Combine(outputDir, $"{fileName}.bytes");
                File.Copy(srcPath, destPath, true);
            }

            AssetDatabase.Refresh();
            Debug.Log("CompileDllAndCopyToGameRes Done!");
        }
    }
}
