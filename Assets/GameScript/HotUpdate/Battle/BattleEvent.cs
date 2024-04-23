using UniFramework.Event;

namespace Game
{
    public static class BattleEvent
    {
        public class BattleEndEvent : IEventMessage
        {
            public UnitCamp winnerCamp;
            public static void SendMsg(UnitCamp winCamp)
            {
                var msg = new BattleEndEvent();
                msg.winnerCamp = winCamp;
                UniEvent.SendMessage(msg);
            }
        }
    }
}
