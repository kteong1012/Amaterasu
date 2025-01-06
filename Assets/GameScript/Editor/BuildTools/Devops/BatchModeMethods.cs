using GameEditor;
using System;
using Unity.VisualScripting.YamlDotNet.Core.Tokens;
using UnityEditor;
using UnityEngine;

public class BatchModeMethods
{
    public static void BuildWindows()
    {
        var buildParameters = LoadFromEnvironmentVariables();

        var pipelineType = BuildPipelineAliasCollection.GetPipelineType(buildParameters.pipelineName);

        if (pipelineType == null)
        {
            throw new Exception($"未找到名为{buildParameters.pipelineName}的构建流程");
        }

        var pipeline = Activator.CreateInstance(pipelineType) as IBuildPipline;
        if (pipeline == null)
        {
            EditorUtility.DisplayDialog("错误", "构建流程实力创建失败", "确定");
            return;
        }

        pipeline.Build(buildParameters);
    }
    private static BuildParameters LoadFromEnvironmentVariables()
    {
        string Get(string name)
        {
            var value = Environment.GetEnvironmentVariable($"{name}");
            Debug.Log($"获取环境变量: {name} = {value}");
            return value;
        }

        var buildParameters = ScriptableObject.CreateInstance<BuildParameters>();
        buildParameters.pipelineName = Get("UBP_PipelineName") ?? buildParameters.pipelineName;
        buildParameters.version = Get("UBP_Version") ?? buildParameters.version;
        buildParameters.tag = Get("UBP_Tag") ?? buildParameters.tag;
        buildParameters.isDevelopmentMode = bool.TryParse(Get("UBP_IsDevelopmentMode"), out var isDevMode) ? isDevMode : buildParameters.isDevelopmentMode;
        buildParameters.exportProject = bool.TryParse(Get("UBP_ExportProject"), out var exportProj) ? exportProj : buildParameters.exportProject;
        buildParameters.enableHotupdate = bool.TryParse(Get("UBP_EnableHotupdate"), out var enableHot) ? enableHot : buildParameters.enableHotupdate;
        buildParameters.cdnHostUrl = Get("UBP_CdnHostUrl") ?? buildParameters.cdnHostUrl;
        buildParameters.hotUpdateAssemblies = Get("UBP_HotUpdateAssemblies")?.Split(';') ?? buildParameters.hotUpdateAssemblies;
        buildParameters.patchAotDllNames = Get("UBP_PatchAotDllNames")?.Split(';') ?? buildParameters.patchAotDllNames;
        buildParameters.mainPackageName = Get("UBP_MainPackageName") ?? buildParameters.mainPackageName;
        buildParameters.rawFilePackageName = Get("UBP_RawFilePackageName") ?? buildParameters.rawFilePackageName;

        return buildParameters;
    }
}