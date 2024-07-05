using UnityEditor;
using UnityEngine;

namespace GameEditor
{
    [BuildTask(BuildParamGroup.Player)]
    public class BuildTask_BuildWindows64Player : IBuildTask
    {

        public void Run(BuildContext context)
        {
            var buildOptions = BuildOptions.None;
            if (context.BuildParameters.isDevelopmentMode)
            {
                buildOptions |= BuildOptions.Development;
                buildOptions |= BuildOptions.AllowDebugging;
            }

            var scnenes = new string[] { context.LaunchScenePath };
            var outputPath = context.BuildParameters.GetBuildOutputPath();
            var buildTarget = context.BuildParameters.BuildTarget;

            UnityEditor.WindowsStandalone.UserBuildSettings.createSolution = context.BuildParameters.exportProject;

            context.BuildReport = BuildPipeline.BuildPlayer(scnenes, outputPath, buildTarget, buildOptions);

            // if it is not batch mode, open the output folder
            if (!Application.isBatchMode)
            {
                var directory = System.IO.Path.GetDirectoryName(outputPath);
                System.Diagnostics.Process.Start(directory);
            }
        }
    }
}
