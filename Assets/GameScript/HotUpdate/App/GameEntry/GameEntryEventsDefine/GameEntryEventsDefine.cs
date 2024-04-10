using UniFramework.Event;

namespace Game
{ 
    public class GameEntryEventsDefine
    {
        public class Login : IEventMessage
        {
            public static void SendEventMessage()
            {
                var msg = new Login();
                UniEvent.SendMessage(msg);
            }
        }

        public class Logout : IEventMessage
        {
            public static void SendEventMessage()
            {
                var msg = new Logout();
                UniEvent.SendMessage(msg);
            }
        }
    }
}