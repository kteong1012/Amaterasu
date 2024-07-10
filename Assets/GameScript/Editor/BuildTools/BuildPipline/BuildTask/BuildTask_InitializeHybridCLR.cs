using HybridCLR.Editor;
using HybridCLR.Editor.Installer;
using HybridCLR.Editor.Settings;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace GameEditor
{
    [BuildTask(BuildParamGroup.Any)]
    public class BuildTask_InitializeHybridCLR : IBuildTask
    {
        public void Run(BuildContext context)
        {
            InstallHybridCLRIfNeed();

            HybridCLRSettings.Instance.hotUpdateAssemblies = context.BuildParameters.hotUpdateAssemblies;
            HybridCLRSettings.Instance.hotUpdateAssemblyDefinitions = new UnityEditorInternal.AssemblyDefinitionAsset[0];
        }

        private static void InstallHybridCLRIfNeed()
        {
            var installController = new InstallerController();
            var version = installController.InstalledLibil2cppVersion;
            if (string.IsNullOrEmpty(version) || version != installController.PackageVersion || !installController.HasInstalledHybridCLR())
            {
                // clear HybridCLRDataDir
                if (Directory.Exists(SettingsUtil.HybridCLRDataDir))
                {
                    Directory.Delete(SettingsUtil.HybridCLRDataDir, true);
                }

                Debug.Log($"Install HybridCLR, version:{installController.PackageVersion}");
                installController.InstallDefaultHybridCLR();
            }
        }
    }
}
