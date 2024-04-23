using Cysharp.Threading.Tasks;
using Game.UI.UIHome;
using Game.UI.UILogin;
using System;
using UniFramework.Event;
using UnityEngine;
using YIUIFramework;
using YooAsset;

namespace Game
{
#pragma warning disable LOG003 // 禁止使用YooAssets.LoadSceneAsync
    [GameService(GameServiceLifeSpan.Game)]
    public class SceneService : GameService
    {
        protected override async UniTask Awake()
        {
            await base.Awake();
        }

        public async UniTask ChangeToBattleScene()
        {
            var handle = YooAssets.LoadSceneAsync("scene_battle");
            var tcs = new UniTaskCompletionSource();
            handle.Completed += (sceneHandle) =>
            {
                tcs.TrySetResult();
            };
            await tcs.Task;
            MainCamera.Instance.Camera.transform.position = new Vector3(0, 40, -4);
            MainCamera.Instance.Camera.transform.rotation = Quaternion.Euler(85, 0, 0);
        }

        public async UniTask ChangeToLoginScene()
        {
            await ChangeScene("scene_login");
            PanelMgr.Inst.OpenPanel<UILoginPanel>();
            MainCamera.Instance.Camera.transform.position = Vector3.zero;
            MainCamera.Instance.Camera.transform.rotation = Quaternion.identity;
        }

        public async UniTask ChangeToHomeScene()
        {
            await ChangeScene("scene_home");
            PanelMgr.Inst.OpenPanel<UIHomePanel>();
            MainCamera.Instance.Camera.transform.position = Vector3.zero;
            MainCamera.Instance.Camera.transform.rotation = Quaternion.identity;
        }

        private async UniTask ChangeScene(string sceneName)
        {
            await YooAssets.LoadSceneAsync(sceneName);
            await UniTask.DelayFrame(2); //等待一帧，确保场景加载完成
        }
    }
#pragma warning restore LOG003 // 禁止使用YooAssets.LoadSceneAsync
}