using UniFramework.Event;

namespace Game
{
    public static class SceneEventDefine
    {
        public class ChangeScene : IEventMessage
        {
            public string SceneName;

            public static void SendMsg(string sceneName)
            {
                var msg = new ChangeScene();
                msg.SceneName = sceneName;
                UniEvent.SendMessage(msg);
            }
        }
    }
}