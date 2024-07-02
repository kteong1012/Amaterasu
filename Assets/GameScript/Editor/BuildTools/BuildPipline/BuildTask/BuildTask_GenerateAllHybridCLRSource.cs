using HybridCLR.Editor.Commands;
using HybridCLR.Editor.Settings;

namespace GameEditor
{

    [BuildTask(BuildParamGroup.Any)]
    public class BuildTask_GenerateAllHybridCLRSource : IBuildTask
    {
        public void Run(BuildContext context)
        {
            PrebuildCommand.GenerateAll();
        }
    }
}
