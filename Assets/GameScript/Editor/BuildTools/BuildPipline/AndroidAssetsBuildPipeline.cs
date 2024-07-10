using System.Collections.Generic;
using UnityEditor;

namespace GameEditor
{
    [BuildPipeline("构建安卓资源", BuildTargetGroup.Android, BuildParamGroup.Assets)]
    public class AndroidAssetsBuildPipeline : IBuildPipline
    {
        public void Build(BuildParameters parameters)
        {
            var context = new BuildContext();
            context.Initialize(this, parameters);

            var tasks = new List<IBuildTask>();
            tasks.Add(new BuildTask_SetBuildEnvironment());
            tasks.Add(new BuildTask_InitializeHybridCLR());
            tasks.Add(new BuildTask_GenerateHybridCLRHotupdateDlls());
            tasks.Add(new BuildTask_BuildYooAsset());

            var gameBuilder = new GameBuilder();
            gameBuilder.Build(context, tasks);
        }
    }
}
