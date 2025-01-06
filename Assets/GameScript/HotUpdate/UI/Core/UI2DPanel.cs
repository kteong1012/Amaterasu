using System.Runtime.CompilerServices;
using UniFramework.Event;

namespace Game
{
    public abstract class UI2DPanel : UI2D
    {
        protected EventGroup _eventGroup = new EventGroup();

        public virtual UI2DPanelLayer Layer { get; } = UI2DPanelLayer.Normal;
        public virtual UI2DPanelOptions Options { get; } = UI2DPanelOptions.None;
        public virtual bool IsAutoRelease => true;

        private string _className;
        public string ClassName
        {
            get
            {
                if (string.IsNullOrEmpty(_className))
                {
                    _className = GetType().FullName;
                }
                return _className;
            }
        }

        protected void Close()
        {
            UIEventDefine.UI2DPanelCloseEvent.SendMsg(this);
        }
    }
}
