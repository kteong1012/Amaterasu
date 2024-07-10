using HybridCLR.Editor;
using Luban;
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
            var aotDllDir = SettingsUtil.GetAssembliesPostIl2CppStripDir(target);
            var outputDir = "Assets/Resources/AotDlls";
            if (Directory.Exists(outputDir))
            {
                Directory.Delete(outputDir, true);
            }
            Directory.CreateDirectory(outputDir);
            var fileName = "AotPatchAssemblies.bytes";

            var buf = new ByteBuf();

            // write count
            buf.WriteSize(context.AotPatchAssemblies.Count);

            foreach (var aotPatchAssembly in context.AotPatchAssemblies)
            {
                var aotDllPath = Path.Combine(aotDllDir, aotPatchAssembly);
                if (!File.Exists(aotDllPath))
                {
                    Debug.LogError($"AotDll not found: {aotDllPath}");
                    continue;
                }
                // write name
                buf.WriteString(aotPatchAssembly);

                // write bytes
                var bytes = File.ReadAllBytes(aotDllPath);
                Debug.Log($"Copy AotDll: {aotDllPath}, size: {bytes.Length}");
                buf.WriteBytes(bytes);
            }

            var bytesData = buf.CopyData();
            var outputPath = Path.Combine(outputDir, fileName);
            File.WriteAllBytes(outputPath, bytesData);
        }
    }
}
