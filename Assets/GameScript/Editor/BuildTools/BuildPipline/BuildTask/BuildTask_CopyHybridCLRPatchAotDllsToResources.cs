using HybridCLR.Editor;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace GameEditor
{
    [BuildTask(BuildParamGroup.Player)]
    public class BuildTask_CopyHybridCLRPatchAotDllsToResources : IBuildTask
    {
        public void Run(BuildContext context)
        {
            CopyAotDllsToResources(context);

            AssetDatabase.Refresh();
        }

        public static void CopyAotDllsToResources(BuildContext context)
        {
            var target = EditorUserBuildSettings.activeBuildTarget;
            var aotDllDir = SettingsUtil.AssembliesPostIl2CppStripDir;
            var outputDir = "Assets/Resources/AotDlls";
            if (Directory.Exists(outputDir))
            {
                Directory.Delete(outputDir, true);
            }
            Directory.CreateDirectory(outputDir);

            var aotDllNames = AOTGenericReferences.PatchedAOTAssemblyList.Concat(context.BuildParameters.patchAotDllNames).Distinct();
            foreach (var assName in aotDllNames)
            {
                var srcPath = $"{aotDllDir}/{target}/{assName}";
                if (!File.Exists(srcPath))
                {
                    Debug.LogWarning($"AotDll not found: {srcPath}");
                    continue;
                }
                var destPath = $"{outputDir}/{assName}.bytes";
                File.Copy(srcPath, destPath);
            }
            AssetDatabase.Refresh();
            Debug.Log("CopyAotDllsToResources Done!");
        }
    }
}
