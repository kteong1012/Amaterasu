
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------


namespace GameEditor.ConfigEditor.Model.Counter
{
    public enum RefreshRule
    {
        /// <summary>
        /// 无刷新时机
        /// </summary>
        NoRefreshTime = 0,
        /// <summary>
        /// 新游戏刷新
        /// </summary>
        NewGame = 1,
    }

    public enum RefreshRule_Alias
    {
        无刷新时机 = 0,
        新游戏刷新 = 1,
    }

    public static class RefreshRule_Metadata
    {
        public static readonly Luban.EditorEnumItemInfo NoRefreshTime = new Luban.EditorEnumItemInfo("NoRefreshTime", "无刷新时机", 0, "无刷新时机");
        public static readonly Luban.EditorEnumItemInfo NewGame = new Luban.EditorEnumItemInfo("NewGame", "新游戏刷新", 1, "新游戏刷新");

        private static readonly System.Collections.Generic.List<Luban.EditorEnumItemInfo> __items = new System.Collections.Generic.List<Luban.EditorEnumItemInfo>
        {
            NoRefreshTime,
            NewGame,
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
