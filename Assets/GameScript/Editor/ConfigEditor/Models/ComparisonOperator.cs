
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------


namespace GameEditor.ConfigEditor.Model
{
    public enum ComparisonOperator
    {
        /// <summary>
        /// 大于
        /// </summary>
        Greater = 0,
        /// <summary>
        /// 大于等于
        /// </summary>
        GreaterEqual = 1,
        /// <summary>
        /// 等于
        /// </summary>
        Equal = 2,
        /// <summary>
        /// 小于
        /// </summary>
        Less = 3,
        /// <summary>
        /// 小于等于
        /// </summary>
        LessEqual = 4,
    }

    public enum ComparisonOperator_Alias
    {
        大于 = 0,
        大于等于 = 1,
        等于 = 2,
        小于 = 3,
        小于等于 = 4,
    }

    public static class ComparisonOperator_Metadata
    {
        public static readonly Luban.EditorEnumItemInfo Greater = new Luban.EditorEnumItemInfo("Greater", "大于", 0, "大于");
        public static readonly Luban.EditorEnumItemInfo GreaterEqual = new Luban.EditorEnumItemInfo("GreaterEqual", "大于等于", 1, "大于等于");
        public static readonly Luban.EditorEnumItemInfo Equal = new Luban.EditorEnumItemInfo("Equal", "等于", 2, "等于");
        public static readonly Luban.EditorEnumItemInfo Less = new Luban.EditorEnumItemInfo("Less", "小于", 3, "小于");
        public static readonly Luban.EditorEnumItemInfo LessEqual = new Luban.EditorEnumItemInfo("LessEqual", "小于等于", 4, "小于等于");

        private static readonly System.Collections.Generic.List<Luban.EditorEnumItemInfo> __items = new System.Collections.Generic.List<Luban.EditorEnumItemInfo>
        {
            Greater,
            GreaterEqual,
            Equal,
            Less,
            LessEqual,
        };

        public static System.Collections.Generic.List<Luban.EditorEnumItemInfo> GetItems() => __items;

        public static Luban.EditorEnumItemInfo GetByName(string name)
        {
            return __items.Find(c => c.Name == name);
        }

        public static Luban.EditorEnumItemInfo GetByNameOrAlias(string name)
        {
            return __items.Find(c => c.Name == name || c.Alias == name);
        }

        public static Luban.EditorEnumItemInfo GetByValue(int value)
        {
            return __items.Find(c => c.Value == value);
        }
    }

} 
