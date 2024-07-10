using System.Collections.Generic;
using UnityEditor;

namespace GameEditor
{
    [BuildPipeline("构建Windows资源和整包", BuildTargetGroup.Standalone, BuildParamGroup.Assets | BuildParamGroup.Player)]
    public class StandalonePlayerAndAssetsBuildPipeline : IBuildPipline
    {
        public void Build(BuildParameters parameters)
        {
            var context = new BuildContext();
            context.Initialize(this, parameters);

            var tasks = new List<IBuildTask>();

            tasks.Add(new BuildTask_SetBuildEnvironment());
            tasks.Add(new BuildTask_SetAppConfig());
            tasks.Add(new BuildTask_InitializeHybridCLR());
            tasks.Add(new BuildTask_GenerateAllHybridCLRSource());
            tasks.Add(new BuildTask_GenerateHybridCLRHotupdateDlls());
            tasks.Add(new BuildTask_CopyHybridCLRPatchAotDllsToResources());
            tasks.Add(new BuildTask_BuildYooAsset());
            tasks.Add(new BuildTask_BuildWindows64Player());

            var gameBuilder = new GameBuilder();
            gameBuilder.Build(context, tasks);
        }
    }
}
