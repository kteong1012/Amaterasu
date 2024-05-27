using UniFramework.Event;

namespace Game
{
    public static class UIEventDefine
    {
        public class UI2DPanelCloseEvent : IEventMessage
        {
            public UI2DPanel Panel { get; set; }

            public static void SendMsg(UI2DPanel panel)
            {
                var e = new UI2DPanelCloseEvent();
                e.Panel = panel;
                UniEvent.SendMessage(e);
            }
        }
    }
    public abstract class UI2DPanel : UI2D
    {
        protected EventGroup _eventGroup = new EventGroup();

        public abstract UI2DPanelLayer Layer { get; }
        public abstract UI2DPanelOptions Options { get; }

        public void Open()
        {
            OnOpen();
        }

        public void Close()
        {
            UIEventDefine.UI2DPanelCloseEvent.SendMsg(this);
        }

        /// <summary>
        /// 创建时调用
        /// </summary>
        public virtual void OnCreate()
        {

        }

        /// <summary>
        /// 每次打开面板时调用
        /// </summary>
        protected virtual void OnOpen()
        {

        }

        /// <summary>
        /// 每次关闭面板时调用
        /// </summary>
        protected virtual void OnClose()
        {

        }

        /// <summary>
        /// 销毁时调用
        /// </summary>
        public virtual void OnRelease()
        {
        }
    }
}
