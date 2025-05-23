
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------


namespace GameEditor.ConfigEditor.Model.BattleAIPickTarget
{
    public enum PickTargetCamp
    {
        /// <summary>
        /// 敌方
        /// </summary>
        Enemy = 0,
        /// <summary>
        /// 我方
        /// </summary>
        Self = 1,
    }

    public enum PickTargetCamp_Alias
    {
        敌方 = 0,
        我方 = 1,
    }

    public static class PickTargetCamp_Metadata
    {
        public static readonly Luban.EditorEnumItemInfo Enemy = new Luban.EditorEnumItemInfo("Enemy", "敌方", 0, "敌方");
        public static readonly Luban.EditorEnumItemInfo Self = new Luban.EditorEnumItemInfo("Self", "我方", 1, "我方");

        private static readonly System.Collections.Generic.List<Luban.EditorEnumItemInfo> __items = new System.Collections.Generic.List<Luban.EditorEnumItemInfo>
        {
            Enemy,
            Self,
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

