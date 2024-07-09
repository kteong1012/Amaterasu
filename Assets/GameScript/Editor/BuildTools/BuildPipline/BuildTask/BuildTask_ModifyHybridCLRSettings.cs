using HybridCLR.Editor.Settings;
using UnityEditor;

namespace GameEditor
{
    [BuildTask(BuildParamGroup.Any)]
    public class BuildTask_ModifyHybridCLRSettings : IBuildTask
    {
        public void Run(BuildContext context)
        {
            HybridCLRSettings.Instance.hotUpdateAssemblies = context.BuildParameters.hotUpdateAssemblies;
            HybridCLRSettings.Instance.hotUpdateAssemblyDefinitions = new UnityEditorInternal.AssemblyDefinitionAsset[0];
        }
    }
}
