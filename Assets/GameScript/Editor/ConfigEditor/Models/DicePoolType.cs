
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
    /// 骰子池类型
    /// </summary>
    public enum DicePoolType
    {
        /// <summary>
        /// 额外
        /// </summary>
        Extra = 0,
        /// <summary>
        /// 力量
        /// </summary>
        STR = 1,
        /// <summary>
        /// 体质
        /// </summary>
        CONS = 2,
        /// <summary>
        /// 敏捷
        /// </summary>
        DEX = 3,
        /// <summary>
        /// 智力
        /// </summary>
        INTELL = 4,
        /// <summary>
        /// 感知
        /// </summary>
        WIS = 5,
        /// <summary>
        /// 魅力
        /// </summary>
        CHA = 6,
        /// <summary>
        /// 武器
        /// </summary>
        Weapon = 7,
        /// <summary>
        /// 护甲
        /// </summary>
        Armor = 8,
        /// <summary>
        /// 状态
        /// </summary>
        Status = 9,
    }

    public enum DicePoolType_Alias
    {
        额外 = 0,
        力量 = 1,
        体质 = 2,
        敏捷 = 3,
        智力 = 4,
        感知 = 5,
        魅力 = 6,
        武器 = 7,
        护甲 = 8,
        状态 = 9,
    }

    public static class DicePoolType_Metadata
    {
        public static readonly Luban.EditorEnumItemInfo Extra = new Luban.EditorEnumItemInfo("Extra", "额外", 0, "额外");
        public static readonly Luban.EditorEnumItemInfo STR = new Luban.EditorEnumItemInfo("STR", "力量", 1, "力量");
        public static readonly Luban.EditorEnumItemInfo CONS = new Luban.EditorEnumItemInfo("CONS", "体质", 2, "体质");
        public static readonly Luban.EditorEnumItemInfo DEX = new Luban.EditorEnumItemInfo("DEX", "敏捷", 3, "敏捷");
        public static readonly Luban.EditorEnumItemInfo INTELL = new Luban.EditorEnumItemInfo("INTELL", "智力", 4, "智力");
        public static readonly Luban.EditorEnumItemInfo WIS = new Luban.EditorEnumItemInfo("WIS", "感知", 5, "感知");
        public static readonly Luban.EditorEnumItemInfo CHA = new Luban.EditorEnumItemInfo("CHA", "魅力", 6, "魅力");
        public static readonly Luban.EditorEnumItemInfo Weapon = new Luban.EditorEnumItemInfo("Weapon", "武器", 7, "武器");
        public static readonly Luban.EditorEnumItemInfo Armor = new Luban.EditorEnumItemInfo("Armor", "护甲", 8, "护甲");
        public static readonly Luban.EditorEnumItemInfo Status = new Luban.EditorEnumItemInfo("Status", "状态", 9, "状态");

        private static readonly System.Collections.Generic.List<Luban.EditorEnumItemInfo> __items = new System.Collections.Generic.List<Luban.EditorEnumItemInfo>
        {
            Extra,
            STR,
            CONS,
            DEX,
            INTELL,
            WIS,
            CHA,
            Weapon,
            Armor,
            Status,
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
