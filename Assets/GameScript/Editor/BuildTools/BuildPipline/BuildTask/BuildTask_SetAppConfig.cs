using Game;
using Game.Log;
using System;
using UnityEngine;

namespace GameEditor
{
    [BuildTask(BuildParamGroup.Player)]
    public class BuildTask_SetAppConfig : IBuildTask
    {
        public void Run(BuildContext context)
        {
            SaveVersionConfiguration(context);
        }

        private static void SaveVersionConfiguration(BuildContext context)
        {
            var versionConfig = context.BuildParameters.CreateAppConfiguration();
            var json = JsonUtility.ToJson(versionConfig);
            System.IO.File.WriteAllText("Assets/Resources/appConfig.json", json);
        }
    }
}
