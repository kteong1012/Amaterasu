
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
    /// 技能类型
    /// </summary>
    public enum SkillType
    {
        /// <summary>
        /// 启动式技能
        /// </summary>
        SkillActive = 0,
        /// <summary>
        /// 骰面技能
        /// </summary>
        SkillDiceFace = 1,
    }

    public enum SkillType_Alias
    {
        启动式技能 = 0,
        骰面技能 = 1,
    }

    public static class SkillType_Metadata
    {
        public static readonly Luban.EditorEnumItemInfo SkillActive = new Luban.EditorEnumItemInfo("SkillActive", "启动式技能", 0, "启动式技能");
        public static readonly Luban.EditorEnumItemInfo SkillDiceFace = new Luban.EditorEnumItemInfo("SkillDiceFace", "骰面技能", 1, "骰面技能");

        private static readonly System.Collections.Generic.List<Luban.EditorEnumItemInfo> __items = new System.Collections.Generic.List<Luban.EditorEnumItemInfo>
        {
            SkillActive,
            SkillDiceFace,
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
