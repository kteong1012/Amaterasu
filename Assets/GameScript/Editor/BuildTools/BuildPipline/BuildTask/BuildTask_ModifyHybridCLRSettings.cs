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

            var target = EditorUserBuildSettings.activeBuildTarget;
            var buildTargetGroup = BuildPipeline.GetBuildTargetGroup(target);
            PlayerSettings.SetApiCompatibilityLevel(buildTargetGroup, ApiCompatibilityLevel.NET_4_6);
            PlayerSettings.SetScriptingBackend(buildTargetGroup, ScriptingImplementation.IL2CPP);
        }
    }
}
