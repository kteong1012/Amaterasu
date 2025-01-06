using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Build;

namespace GameEditor
{
    [BuildTask(BuildParamGroup.Any)]
    public class BuildTask_SetBuildEnvironment : IBuildTask
    {
        public void Run(BuildContext context)
        {
            ConfigureBuildSettings(context);

            SetSymbols(context);
        }

        private static void ConfigureBuildSettings(BuildContext context)
        {
            EditorUserBuildSettings.development = context.BuildParameters.isDevelopmentMode;

            var target = EditorUserBuildSettings.activeBuildTarget;
            var buildTargetGroup = BuildPipeline.GetBuildTargetGroup(target);
            var namedBuildTarget = NamedBuildTarget.FromBuildTargetGroup(buildTargetGroup);
            PlayerSettings.SetApiCompatibilityLevel(namedBuildTarget, ApiCompatibilityLevel.NET_4_6);
            PlayerSettings.SetScriptingBackend(namedBuildTarget, ScriptingImplementation.IL2CPP);
        }

        private void SetSymbols(BuildContext context)
        {

            var target = EditorUserBuildSettings.activeBuildTarget;
            var buildTargetGroup = UnityEditor.BuildPipeline.GetBuildTargetGroup(target);
            var namedBuildTarget = NamedBuildTarget.FromBuildTargetGroup(buildTargetGroup);
            var rawSymbols = PlayerSettings.GetScriptingDefineSymbols(namedBuildTarget);
            var symbols = rawSymbols.Split(';').Distinct().ToHashSet();

            // Common Basic symbols
            symbols.Add("UNITASK_DOTWEEN_SUPPORT");
            symbols.Add("TextMeshPro");
            symbols.Add("DOTWEEN");

            // LOG_DEBUG
            if (context.BuildParameters.isDevelopmentMode)
            {
                symbols.Add("LOG_DEBUG");
            }
            else
            {
                symbols.Remove("LOG_DEBUG");
            }

            PlayerSettings.SetScriptingDefineSymbols(namedBuildTarget, string.Join(";", symbols));
        }
    }
}
