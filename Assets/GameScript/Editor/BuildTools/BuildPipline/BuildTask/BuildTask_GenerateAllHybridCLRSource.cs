using HybridCLR.Editor.Commands;
using HybridCLR.Editor.Settings;
using UnityEditor;

namespace GameEditor
{

    [BuildTask(BuildParamGroup.Any)]
    public class BuildTask_GenerateAllHybridCLRSource : IBuildTask
    {
        public void Run(BuildContext context)
        {
            var development = context.BuildParameters.isDevelopmentMode;

            var target = EditorUserBuildSettings.activeBuildTarget;
            CompileDllCommand.CompileDll(target, development);
            Il2CppDefGeneratorCommand.GenerateIl2CppDef();

            // 这几个生成依赖HotUpdateDlls
            LinkGeneratorCommand.GenerateLinkXml(target);

            // 生成裁剪后的aot dll
            StripAOTDllCommand.GenerateStripedAOTDlls(target);

            // 桥接函数生成依赖于AOT dll，必须保证已经build过，生成AOT dll
            MethodBridgeGeneratorCommand.GenerateMethodBridge(target);
            ReversePInvokeWrapperGeneratorCommand.GenerateReversePInvokeWrapper(target);
            AOTReferenceGeneratorCommand.GenerateAOTGenericReference(target);
        }
    }
}
