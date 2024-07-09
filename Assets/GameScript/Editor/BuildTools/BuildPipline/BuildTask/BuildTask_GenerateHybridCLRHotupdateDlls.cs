using Game;
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
            CompileDllAndCopyToGameRes(context); ;

            AssetDatabase.Refresh();
        }
        public static void CompileDllAndCopyToGameRes(BuildContext contenxt)
        {
            var target = EditorUserBuildSettings.activeBuildTarget;
            CompileDllCommand.CompileDll(target, contenxt.BuildParameters.isDevelopmentMode);

            var hotUpdateDllDir = SettingsUtil.GetHotUpdateDllsOutputDirByTarget(target);
            var codeDir = "Assets/GameRes/Raw/Code";
            if (Directory.Exists(codeDir))
            {
                Directory.Delete(codeDir, true);
            }
            Directory.CreateDirectory(codeDir);

            foreach (var dllName in contenxt.BuildParameters.hotUpdateAssemblies)
            {
                var fileName = $"{dllName}.dll";
                var srcPath = $"{hotUpdateDllDir}/{fileName}";
                var destPath = Path.Combine(codeDir, $"{fileName}.bytes");
                File.Copy(srcPath, destPath, true);
            }

            var csharpConfigFileName = "csharpconfig.json";
            var csharpConfigPath = $"{codeDir}/{csharpConfigFileName}";
            var csharpConfig = (CSharpConfiguration)null;
            if (File.Exists(csharpConfigPath))
            {
                var json = File.ReadAllText(csharpConfigPath);
                csharpConfig = JsonUtility.FromJson<CSharpConfiguration>(json);
            }
            else
            {
                csharpConfig = new CSharpConfiguration();
            }
            csharpConfig.csharpVersion = contenxt.BuildParameters.version;
            csharpConfig.hotupdateAssemblies = contenxt.BuildParameters.hotUpdateAssemblies;

            var csharpConfigJson = JsonUtility.ToJson(csharpConfig);
            File.WriteAllText(csharpConfigPath, csharpConfigJson);

            AssetDatabase.Refresh();
            Debug.Log("CompileDllAndCopyToGameRes Done!");
        }
    }
}
