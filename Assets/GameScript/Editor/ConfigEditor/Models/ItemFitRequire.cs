
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------


namespace GameEditor.ConfigEditor.Model
{
    /// <summary>
    /// 装配要求
    /// </summary>
    public enum ItemFitRequire
    {
        /// <summary>
        /// 大于等于
        /// </summary>
        Greater = 0,
        /// <summary>
        /// 小于等于
        /// </summary>
        Less = 1,
        /// <summary>
        /// 等于
        /// </summary>
        Equal = 2,
        /// <summary>
        /// 无要求
        /// </summary>
        NoRequirement = 3,
    }

    public enum ItemFitRequire_Alias
    {
        大于等于 = 0,
        小于等于 = 1,
        等于 = 2,
        无要求 = 3,
    }

    public static class ItemFitRequire_Metadata
    {
        public static readonly Luban.EditorEnumItemInfo Greater = new Luban.EditorEnumItemInfo("Greater", "大于等于", 0, "大于等于");
        public static readonly Luban.EditorEnumItemInfo Less = new Luban.EditorEnumItemInfo("Less", "小于等于", 1, "小于等于");
        public static readonly Luban.EditorEnumItemInfo Equal = new Luban.EditorEnumItemInfo("Equal", "等于", 2, "等于");
        public static readonly Luban.EditorEnumItemInfo NoRequirement = new Luban.EditorEnumItemInfo("NoRequirement", "无要求", 3, "无要求");

        private static readonly System.Collections.Generic.List<Luban.EditorEnumItemInfo> __items = new System.Collections.Generic.List<Luban.EditorEnumItemInfo>
        {
            Greater,
            Less,
            Equal,
            NoRequirement,
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

