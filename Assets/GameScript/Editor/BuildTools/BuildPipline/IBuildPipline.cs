using System.Collections.Generic;
using UnityEditor;

namespace GameEditor
{
    public interface IBuildPipline
    {
        void Build(BuildParameters buildParameters);
    }

    [System.AttributeUsage(System.AttributeTargets.Class)]
    public class BuildPipelineAttribute : System.Attribute
    {
        public string Name { get; }
        public BuildTargetGroup TargetGroup { get; }
        public BuildParamGroup ParamGroup { get; }
        public BuildPipelineAttribute(string name, BuildTargetGroup targetGroup, BuildParamGroup buildParamGroup)
        {
            Name = name;
            TargetGroup = targetGroup;
            ParamGroup = buildParamGroup;
        }
    }
}
