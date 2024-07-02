using UnityEditor.Build.Reporting;

namespace GameEditor
{
    public class BuildContext
    {
        public string LaunchScenePath => "Assets/Scenes/Launch.unity";

        public IBuildPipline Pipeline { get; private set; }

        // 只读属性
        public BuildParameters BuildParameters { get; private set; }

        // 可读写属性
        public BuildReport BuildReport { get; set; }

        public void Initialize(IBuildPipline pipeline, BuildParameters buildParameters)
        {
            Pipeline = pipeline;
            BuildParameters = buildParameters;
            OnInit(buildParameters);
        }

        protected virtual void OnInit(BuildParameters buildParameters)
        {
        }
    }
}
