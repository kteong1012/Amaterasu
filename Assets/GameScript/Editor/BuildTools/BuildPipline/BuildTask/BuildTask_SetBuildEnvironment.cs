using System.Collections.Generic;
using System.Linq;
using UnityEditor;

namespace GameEditor
{
    [BuildTask(BuildParamGroup.Any)]
    public class BuildTask_SetBuildEnvironment : IBuildTask
    {
        private readonly HashSet<string> _symbols;

        public BuildTask_SetBuildEnvironment()
        {
            var target = EditorUserBuildSettings.activeBuildTarget;
            var buildTargetGroup = UnityEditor.BuildPipeline.GetBuildTargetGroup(target);
            var rawSymbols = PlayerSettings.GetScriptingDefineSymbolsForGroup(buildTargetGroup);
            _symbols = rawSymbols.Split(';').Distinct().ToHashSet();
        }

        public void Run(BuildContext context)
        {
            EditorUserBuildSettings.development = context.BuildParameters.isDevelopmentMode;

            var target = EditorUserBuildSettings.activeBuildTarget;
            var buildTargetGroup = BuildPipeline.GetBuildTargetGroup(target);
            PlayerSettings.SetApiCompatibilityLevel(buildTargetGroup, ApiCompatibilityLevel.NET_4_6);
            PlayerSettings.SetScriptingBackend(buildTargetGroup, ScriptingImplementation.IL2CPP);

            SetLogSymbol(context);
        }

        private void AddSymbol(string symbol)
        {
            _symbols.Add(symbol);
        }

        private void RemoveSymbol(string symbol)
        {
            _symbols.Remove(symbol);
        }

        private void SetLogSymbol(BuildContext context)
        {
            if (context.BuildParameters.isDevelopmentMode)
            {
                AddSymbol("LOG_DEBUG");
            }
            else
            {
                RemoveSymbol("LOG_DEBUG");
            }
        }
    }
}
