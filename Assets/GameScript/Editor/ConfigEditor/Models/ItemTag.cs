
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
    /// 物品的Tag
    /// </summary>
    public enum ItemTag
    {
        /// <summary>
        /// 剑
        /// </summary>
        Sword = 0,
        /// <summary>
        /// 盾
        /// </summary>
        Shield = 1,
    }

    public enum ItemTag_Alias
    {
        剑 = 0,
        盾 = 1,
    }

    public static class ItemTag_Metadata
    {
        public static readonly Luban.EditorEnumItemInfo Sword = new Luban.EditorEnumItemInfo("Sword", "剑", 0, "剑");
        public static readonly Luban.EditorEnumItemInfo Shield = new Luban.EditorEnumItemInfo("Shield", "盾", 1, "盾");

        private static readonly System.Collections.Generic.List<Luban.EditorEnumItemInfo> __items = new System.Collections.Generic.List<Luban.EditorEnumItemInfo>
        {
            Sword,
            Shield,
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
