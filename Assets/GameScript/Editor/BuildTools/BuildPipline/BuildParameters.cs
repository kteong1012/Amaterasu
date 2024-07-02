using Game;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace GameEditor
{
    [Flags]
    public enum BuildParamGroup
    {
        Any = 0,
        Player = 1 << 10,
        Assets = 1 << 11,
    }

    [AttributeUsage(AttributeTargets.Field)]
    public class BuildParamGroupAttribute : System.Attribute
    {
        public BuildParamGroup group;

        public BuildParamGroupAttribute(BuildParamGroup group)
        {
            this.group = group;
        }
    }


    /// <summary>
    /// 都是只读字段，所以需要构造时候一次性初始化所有参数
    /// </summary>
    public class BuildParameters : ScriptableObject
    {
        [HideInInspector]
        public BuildTarget BuildTarget => EditorUserBuildSettings.activeBuildTarget;

        [Header("构建管线")]
        [BuildParamGroup(BuildParamGroup.Any)]
        public string pipelineName = string.Empty;

        [Header("版本号")]
        [BuildParamGroup(BuildParamGroup.Any)]
        public string version;

        [Header("标签")]
        [BuildParamGroup(BuildParamGroup.Any)]
        public string tag;

        [Header("是否开发模式")]
        [BuildParamGroup(BuildParamGroup.Any)]
        public bool isDevelopmentMode;

        [Header("导出为工程")]
        [BuildParamGroup(BuildParamGroup.Player)]
        public bool exportProject;

        [Header("是否启用热更新(取消勾选就是单机包)")]
        [BuildParamGroup(BuildParamGroup.Player)]
        public bool enableHotupdate;

        [Header("CDN服务器地址")]
        [BuildParamGroup(BuildParamGroup.Player)]
        public string cdnHostUrl;

        [Header("热更新的程序集")]
        [BuildParamGroup(BuildParamGroup.Assets)]
        public string[] hotUpdateAssemblies;

        [Header("补充元数据程序集名")]
        [BuildParamGroup(BuildParamGroup.Player)]
        public string[] patchAotDllNames;

        [Header("主资源包包名")]
        [BuildParamGroup(BuildParamGroup.Player)]
        public string mainPackageName;

        [Header("原生文件资源包包名")]
        [BuildParamGroup(BuildParamGroup.Player)]
        public string rawFilePackageName;



        public static void AddRange(List<string> fieldNames, BuildParamGroup group)
        {
            var type = typeof(BuildParameters);
            var fields = type.GetFields();
            foreach (var field in fields)
            {
                var attr = field.GetCustomAttribute<BuildParamGroupAttribute>();
                // 没有标记的字段默认不过滤
                if (attr == null)
                {
                    if (!fieldNames.Contains(field.Name))
                    {
                        fieldNames.Add(field.Name);
                    }
                    continue;
                }

                if (attr.group.HasFlag(group))
                {
                    if (!fieldNames.Contains(field.Name))
                    {
                        fieldNames.Add(field.Name);
                    }
                }
            }
        }
    }
    public static class BuildParametersExtensions
    {
        public static BuildTargetGroup GetBuildTargetGroup(this BuildParameters buildParameters)
        {
            return BuildPipeline.GetBuildTargetGroup(buildParameters.BuildTarget);
        }

        public static string GetBuildOutputPath(this BuildParameters buildParameters)
        {
            var buildFolder = $"Build/{buildParameters.BuildTarget}";
            var appName = GetAppOutputName(buildParameters);

            if (buildParameters.BuildTarget is BuildTarget.Android)
            {
                if (buildParameters.exportProject)
                {
                    return Path.Combine(buildFolder, "AndroidProject");
                }
                else
                {
                    return Path.Combine(buildFolder, "Apk", $"{appName}.apk");
                }
            }
            else if (buildParameters.BuildTarget is BuildTarget.StandaloneWindows or BuildTarget.StandaloneWindows64)
            {
                if (buildParameters.exportProject)
                {
                    return Path.Combine(buildFolder, "SolutionProject");
                }
                else
                {
                    return Path.Combine(buildFolder, appName);
                }
            }
            else if (buildParameters.BuildTarget is BuildTarget.iOS)
            {
                return Path.Combine(buildFolder, "xcode");
            }

            throw new System.InvalidOperationException("构建参数错误，无法获得到处工程路径");
        }

        private static string GetAppOutputName(BuildParameters parameters)
        {
            if (!Application.isBatchMode)
            {
                return PlayerSettings.productName;
            }

            var areas = new List<string>();
            if (!string.IsNullOrEmpty(parameters.tag))
            {
                areas.Add(parameters.tag);
            }
            areas.Add(DateTime.Now.ToString("yyyyMMdd"));
            areas.Add(parameters.isDevelopmentMode ? "Debug" : "Release");
            areas.Add(parameters.version);

            // 替换掉非法符号
            var formatedAreas = areas.Select(area =>
            {
                var invalidChars = Path.GetInvalidFileNameChars();
                foreach (var c in invalidChars)
                {
                    area = area.Replace(c, '_');
                }
                return area;
            });

            return string.Join("_", formatedAreas);
        }

        public static AppConfiguration CreateAppConfiguration(this BuildParameters buildParameters)
        {
            return new AppConfiguration
            {
                appVersion = buildParameters.version,
                isDevelopmentMode = buildParameters.isDevelopmentMode,
                enableHotupdate = buildParameters.enableHotupdate,
                cdnHostUrl = buildParameters.cdnHostUrl,
                hotupdateAssemblies = buildParameters.hotUpdateAssemblies,
                aotMetaDlls = buildParameters.patchAotDllNames,
                mainPackageName = buildParameters.mainPackageName,
                rawFilePackageName = buildParameters.rawFilePackageName
            };
        }
    }
}
