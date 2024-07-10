using HybridCLR.Editor;
using HybridCLR.Editor.AOT;
using HybridCLR.Editor.Commands;
using HybridCLR.Editor.Meta;
using HybridCLR.Editor.Settings;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditor.Build;
using UnityEngine;

namespace GameEditor
{

    [BuildTask(BuildParamGroup.Any)]
    public class BuildTask_GenerateAllHybridCLRSource : IBuildTask
    {
        public void Run(BuildContext context)
        {
            var development = context.BuildParameters.isDevelopmentMode;

            var installer = new HybridCLR.Editor.Installer.InstallerController();
            if (!installer.HasInstalledHybridCLR())
            {
                throw new BuildFailedException($"You have not initialized HybridCLR, please install it via menu 'HybridCLR/Installer'");
            }
            BuildTarget target = EditorUserBuildSettings.activeBuildTarget;
            CompileDllCommand.CompileDll(target, development);
            Il2CppDefGeneratorCommand.GenerateIl2CppDef();

            // 这几个生成依赖HotUpdateDlls
            LinkGeneratorCommand.GenerateLinkXml(target);

            // 生成裁剪后的aot dll
            StripAOTDllCommand.GenerateStripedAOTDlls(target);

            // 桥接函数生成依赖于AOT dll，必须保证已经build过，生成AOT dll
            MethodBridgeGeneratorCommand.GenerateMethodBridgeAndReversePInvokeWrapper(target);

            var referencedDllNames = GenerateAOTGenericReference(target);

            // 添加扫描后的dll到AOT裁剪列表
            foreach (var dllName in referencedDllNames)
            {
                context.AotPatchAssemblies.Add(dllName);
            }
            // 添加配置的dll到AOT裁剪列表
            foreach (var dllName in context.BuildParameters.patchAotDllNames)
            {
                var name = dllName;
                if (!name.EndsWith(".dll"))
                {
                    name += ".dll";
                }
                context.AotPatchAssemblies.Add(name);
            }
            Debug.Log("AotPatchAssemblies: " + string.Join(", ", context.AotPatchAssemblies));
            Debug.Log("GenerateAllHybridCLRSource Done!");
        }



        public static IEnumerable<string> GenerateAOTGenericReference(BuildTarget target)
        {
            var referencedDllNames = new HashSet<string>();

            var gs = SettingsUtil.HybridCLRSettings;
            List<string> hotUpdateDllNames = SettingsUtil.HotUpdateAssemblyNamesExcludePreserved;

            AssemblyReferenceDeepCollector collector = new AssemblyReferenceDeepCollector(MetaUtil.CreateHotUpdateAndAOTAssemblyResolver(target, hotUpdateDllNames), hotUpdateDllNames);
            var analyzer = new Analyzer(new Analyzer.Options
            {
                MaxIterationCount = Math.Min(20, gs.maxGenericReferenceIteration),
                Collector = collector,
            });

            analyzer.Run();

            var types = analyzer.AotGenericTypes.ToList();
            var methods = analyzer.AotGenericMethods.ToList();
            var aotPatchListFilePath = $"Assets/Resources/aotPatchList.txt";
            List<dnlib.DotNet.ModuleDef> modules = new HashSet<dnlib.DotNet.ModuleDef>(
                types
                .Select(t => t.Type.Module)
                .Concat(methods.Select(m => m.Method.Module)))
                .ToList();
            modules.Sort((a, b) => a.Name.CompareTo(b.Name));

            foreach (var module in modules)
            {
                referencedDllNames.Add(module.Name);
            }


            return referencedDllNames;
        }
    }
}
