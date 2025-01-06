namespace LocalizationTool
{
    internal class LocalizationTaskAttribute : Attribute
    {
        public string taskName;

        public LocalizationTaskAttribute(string taskName)
        {
            this.taskName = taskName;
        }
    }
}