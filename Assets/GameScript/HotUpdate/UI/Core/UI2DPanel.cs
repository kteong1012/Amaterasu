using UniFramework.Event;

namespace Game
{
    public abstract class UI2DPanel : UI2D
    {
        protected EventGroup _eventGroup = new EventGroup();

        public abstract UI2DPanelLayer Layer { get; }
        public abstract UI2DPanelOptions Options { get; }

        public virtual string ClassName { get; }

        public void Close()
        {
            UIEventDefine.UI2DPanelCloseEvent.SendMsg(this);
        }
    }
}
