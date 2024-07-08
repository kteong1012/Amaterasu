using Game.Log;

namespace Game
{
    [UI2D("UILoginNode")]
    public partial class UILoginNode : UI2DNode
    {
        protected override void OnCreate()
        {
            GameLog.Info("======= UILoginNode OnCreate =======");
        }

        protected override void OnRelease()
        {
            GameLog.Info("======= UILoginNode OnRelease =======");
        }

        protected override void OnShow()
        {
            GameLog.Info("======= UILoginNode OnShow =======");
        }

        protected override void OnHide()
        {
            GameLog.Info("======= UILoginNode OnHide =======");
        }
    }
}
