namespace GameEditor
{
    public interface IBuildTask
    {
        void Run(BuildContext context);
    }

    [System.AttributeUsage(System.AttributeTargets.Class)]
    public class BuildTaskAttribute : System.Attribute
    {
        public BuildParamGroup ParamGroup { get; }

        public BuildTaskAttribute(BuildParamGroup buildParamGroup)
        {
            ParamGroup = buildParamGroup;
        }
    }
}
