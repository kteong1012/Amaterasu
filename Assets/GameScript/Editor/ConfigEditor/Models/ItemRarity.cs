
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
    /// 物品稀有度
    /// </summary>
    public enum ItemRarity
    {
        /// <summary>
        /// 普通
        /// </summary>
        Common = 0,
        /// <summary>
        /// 罕见
        /// </summary>
        Uncommon = 1,
        /// <summary>
        /// 稀有
        /// </summary>
        Rare = 2,
    }

    public enum ItemRarity_Alias
    {
        普通 = 0,
        罕见 = 1,
        稀有 = 2,
    }

    public static class ItemRarity_Metadata
    {
        public static readonly Luban.EditorEnumItemInfo Common = new Luban.EditorEnumItemInfo("Common", "普通", 0, "普通");
        public static readonly Luban.EditorEnumItemInfo Uncommon = new Luban.EditorEnumItemInfo("Uncommon", "罕见", 1, "罕见");
        public static readonly Luban.EditorEnumItemInfo Rare = new Luban.EditorEnumItemInfo("Rare", "稀有", 2, "稀有");

        private static readonly System.Collections.Generic.List<Luban.EditorEnumItemInfo> __items = new System.Collections.Generic.List<Luban.EditorEnumItemInfo>
        {
            Common,
            Uncommon,
            Rare,
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
