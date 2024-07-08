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

        public class UI2DNodeReleaseEvent : IEventMessage
        {
            public UI2DNode Node { get; set; }

            public static void SendMsg(UI2DNode node)
            {
                var e = new UI2DNodeReleaseEvent();
                e.Node = node;
                UniEvent.SendMessage(e);
            }
        }
    }
}
