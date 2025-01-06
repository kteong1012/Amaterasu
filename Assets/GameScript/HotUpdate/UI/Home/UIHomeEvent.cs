using UniFramework.Event;

namespace Game
{
    public class UIHomeEvent
    {
        public class UIPlayerDataChangeEvent : IEventMessage
        {
            public static void SendMsg()
            {
                var e = new UIPlayerDataChangeEvent();
                UniEvent.SendMessage(e);
            }
        }
    }
}
