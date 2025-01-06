using UnityEngine;

namespace Game
{
    // 这个类是游戏的入口类，反射调用LaunchApp的Start方法，不要认为没有被调用
    public static class StartGame
    {
        public static void Start()
        {
            var go = new GameObject("GameEntry");
            go.AddComponent<GameEntry>(); 
        }
    }
}
