using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace Game
{
    public class StartApp
    {
        public async static void Start()
        {
            await StartResourceComponent();

            // Get 'Hotfix' Assembly
            var hotfixAssembly = Assembly.Load("Hotfix");
            var startGameType = hotfixAssembly.GetType("Game.StartGame");
            var startMethod = startGameType.GetMethod("Start", BindingFlags.Static | BindingFlags.Public);
            startMethod.Invoke(null, null);
        }

        private static async UniTask StartResourceComponent()
        {
            var resourceComponent = await G.Ins.RegisterGameComponent<ResourceComponent>();
            await resourceComponent.UpdatePatch();

            // 切换到主页面场景
            SceneEventDefine.ChangeToHomeScene.SendEventMessage();
        }
    }
}
