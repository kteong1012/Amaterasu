using System.Collections.Generic;
using UnityEditor;

namespace GameEditor
{
    [BuildPipeline("构建安卓资源和整包", BuildTargetGroup.Android, BuildParamGroup.Assets | BuildParamGroup.Player)]
    public class AndroidPlayerAndAssetsBuildPipeline : IBuildPipline
    {
        public void Build(BuildParameters parameters)
        {
            var context = new BuildContext();
            context.Initialize(this, parameters);

            var tasks = new List<IBuildTask>();

            tasks.Add(new BuildTask_SetDefineSymbols());
            tasks.Add(new BuildTask_SetAppConfig());
            tasks.Add(new BuildTask_ModifyHybridCLRSettings());
            tasks.Add(new BuildTask_GenerateAllHybridCLRSource());
            tasks.Add(new BuildTask_GenerateHybridCLRHotupdateDlls());
            tasks.Add(new BuildTask_CopyHybridCLRPatchAotDllsToResources());
            tasks.Add(new BuildTask_BuildYooAsset());
            tasks.Add(new BuildTask_BuildAndroidPlayer());

            var gameBuilder = new GameBuilder();
            gameBuilder.Build(context, tasks);
        }
    }
}
