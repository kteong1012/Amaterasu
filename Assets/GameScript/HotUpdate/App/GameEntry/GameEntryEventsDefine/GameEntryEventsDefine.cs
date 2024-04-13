using UniFramework.Event;

namespace Game
{ 
    public class GameEntryEventsDefine
    {
        public class LoginSuccess : IEventMessage
        {
            public LoginChannel LoginChannel { get; private set; }
            public string PlayerId { get; private set; }
            public static void SendEventMessage(LoginChannel loginChannel, string playerId)
            {
                var msg = new LoginSuccess();
                msg.LoginChannel = loginChannel;
                msg.PlayerId = playerId;;
                UniEvent.SendMessage(msg);
            }
        }

        public class LogoutSuccess : IEventMessage
        {
            public LoginChannel LoginChannel { get; private set; }
            public string PlayerId { get; private set; }
            public static void SendEventMessage(LoginChannel loginChannel, string playerId)
            {
                var msg = new LogoutSuccess();
                msg.LoginChannel = loginChannel;
                msg.PlayerId = playerId;
                UniEvent.SendMessage(msg);
            }
        }
    }
}