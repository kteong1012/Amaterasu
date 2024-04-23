using YIUIFramework;

namespace YIUICodeGenerated
{
    /// <summary>
    /// 由YIUI工具自动创建 请勿手动修改
    /// 用法: UIBindHelper.InternalGameGetUIBindVoFunc = YIUICodeGenerated.UIBindProvider.Get;
    /// </summary>
    public static class UIBindProvider
    {
        public static UIBindVo[] Get()
        {
            var BasePanel     = typeof(BasePanel);
            var BaseView      = typeof(BaseView);
            var BaseComponent = typeof(BaseComponent);
            var list          = new UIBindVo[3];
            list[0] = new UIBindVo
            {
                PkgName     = Game.UI.UILogin.UILoginPanelBase.PkgName,
                ResName     = Game.UI.UILogin.UILoginPanelBase.ResName,
                CodeType    = BasePanel,
                BaseType    = typeof(Game.UI.UILogin.UILoginPanelBase),
                CreatorType = typeof(Game.UI.UILogin.UILoginPanel),
            };
            list[1] = new UIBindVo
            {
                PkgName     = Game.UI.UIHome.UIHomePanelBase.PkgName,
                ResName     = Game.UI.UIHome.UIHomePanelBase.ResName,
                CodeType    = BasePanel,
                BaseType    = typeof(Game.UI.UIHome.UIHomePanelBase),
                CreatorType = typeof(Game.UI.UIHome.UIHomePanel),
            };
            list[2] = new UIBindVo
            {
                PkgName     = Game.UI.UIBattleEnd.UIBattleEndPanelBase.PkgName,
                ResName     = Game.UI.UIBattleEnd.UIBattleEndPanelBase.ResName,
                CodeType    = BasePanel,
                BaseType    = typeof(Game.UI.UIBattleEnd.UIBattleEndPanelBase),
                CreatorType = typeof(Game.UI.UIBattleEnd.UIBattleEndPanel),
            };

            return list;
        }
    }
}