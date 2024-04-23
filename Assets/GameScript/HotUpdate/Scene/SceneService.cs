using Cysharp.Threading.Tasks;
using Game.UI.UIHome;
using Game.UI.UILogin;
using System;
using UniFramework.Event;
using UnityEngine;
using YIUIFramework;
using YooAsset;
using static Game.SceneEventDefine;

namespace Game
{
    [GameService(GameServiceDomain.Game)]
    public class SceneService : GameService
    {
        protected override async UniTask Awake()
        {
            await base.Awake();
        }

        public async UniTask ChangeToBattleScene()
        {
            await ChangeScene("scene_battle");
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

#pragma warning disable LOG003 // 禁止使用YooAssets.LoadSceneAsync
        private async UniTask ChangeScene(string sceneName)
        {
            var behavioursToDestroy = GetGameBehavioursInScene();
            foreach (var behaviour in behavioursToDestroy)
            {
                behaviour.StopServices();
            }

            await YooAssets.LoadSceneAsync(sceneName);
            await UniTask.DelayFrame(1); //等待一帧，确保场景加载完成

            var behavioursToStart = GetGameBehavioursInScene();
            foreach (var behaviour in behavioursToStart)
            {
                behaviour.StartServices().Forget();
            }
        }
#pragma warning restore LOG003 // 禁止使用YooAssets.LoadSceneAsync

        private GameServiceBehaviour[] GetGameBehavioursInScene()
        {
            return GameObject.FindObjectsOfType<GameServiceBehaviour>();
        }
    }
}