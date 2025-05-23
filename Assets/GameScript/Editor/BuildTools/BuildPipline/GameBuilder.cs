﻿using Game.Log;
using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor.Build.Pipeline.Utilities;
using UnityEngine;

namespace GameEditor
{
    public class GameBuilder
    {
        public void Build(BuildContext context, List<IBuildTask> tasks)
        {
            var buildParameters = context.BuildParameters;
            // 检查构建参数是否为空
            if (buildParameters == null)
            {
                throw new System.Exception($"{nameof(buildParameters)} is null !");
            }

            // 检查构建上下文是否为空
            if (context == null)
            {
                throw new System.Exception($"{nameof(context)} is null !");
            }

            // 检查构建任务是否为空
            if (tasks.Count == 0)
            {
                throw new System.Exception($"Build pipeline is empty !");
            }

            CheckBuildGroup(context, tasks);

            // 执行构建流程
            foreach (var task in tasks)
            {
                task.Run(context);
            }
        }

        private void CheckBuildGroup(BuildContext context, List<IBuildTask> tasks)
        {
            var pipeline = context.Pipeline;
            var pipelineType = pipeline.GetType();
            var pipelineAttributes = pipelineType.GetCustomAttribute<BuildPipelineAttribute>();
            if (pipelineAttributes == null)
            {
                throw new System.Exception($"类型 {pipelineType.Name} 必须有 BuildPipelineAttribute 特性！");
            }
            var pipelineParamGroup = pipelineAttributes.ParamGroup;

            bool error = false;
            foreach (var task in tasks)
            {
                var taskType = task.GetType();
                var taskAttributes = taskType.GetCustomAttribute<BuildTaskAttribute>();
                if (taskAttributes == null)
                {
                    Debug.LogError($"类型 {taskType.Name} 必须有 BuildTaskAttribute 特性！");
                    error = true;
                    continue;
                }
                var taskParamGroup = taskAttributes.ParamGroup;

                if ((pipelineParamGroup & taskParamGroup) != taskParamGroup)
                {
                    Debug.LogError($" 管线 '{pipelineAttributes.Name}' 的参数组 '{pipelineParamGroup}' 与任务 '{taskType.Name}' 的参数组 '{taskParamGroup}' 不匹配，管线参数组必顿包含任务参数组！");
                    error = true;
                    continue;
                }
            }

            if (error)
            {
                throw new System.Exception("构建流程参数组检查不通过！");
            }
        }
    }
}
