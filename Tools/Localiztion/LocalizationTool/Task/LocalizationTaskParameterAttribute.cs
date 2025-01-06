namespace LocalizationTool
{
    [AttributeUsage(AttributeTargets.Field)]
    internal class LocalizationTaskParameterAttribute : Attribute
    {
        public string name;
        public string description;
        public bool required;

        public LocalizationTaskParameterAttribute(string name, string description = "", bool required = false)
        {
            this.name = name;
            this.description = description;
            this.required = required;
        }
    }
}