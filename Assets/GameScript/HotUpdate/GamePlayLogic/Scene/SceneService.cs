using Cysharp.Threading.Tasks;
using DG.Tweening;
using Game.Log;
using System.Collections.Generic;
using UniFramework.Event;
using UnityEngine;
using YooAsset;

namespace Game
{
    public class StartChangeSceneEvent : IEventMessage
    {
        public string LastSceneName { get; set; }
        public string CurrentSceneName { get; set; }

        public static void SendMsg(string lastSceneName, string currentSceneName)
        {
            UniEvent.SendMessage(new StartChangeSceneEvent()
            {
                LastSceneName = lastSceneName,
                CurrentSceneName = currentSceneName
            });
        }
    }

    public class CompleteChangeSceneEvent : IEventMessage
    {
        public string LastSceneName { get; set; }
        public string CurrentSceneName { get; set; }

        public static void SendMsg(string lastSceneName, string currentSceneName)
        {
            UniEvent.SendMessage(new CompleteChangeSceneEvent()
            {
                LastSceneName = lastSceneName,
                CurrentSceneName = currentSceneName
            });
        }
    }

    [GameService(GameServiceDomain.Game)]
    public partial class SceneService : GameService
    {
        enum Status
        {
            Idle,
            Loading,
        }

        private string _currentSceneName;
        private Status _status;
        private Dictionary<string, (Vector3 cameraPos, Vector3 cameraEuler, float fov)> _sceneCameraTransformConfig = new Dictionary<string, (Vector3 cameraPos, Vector3 cameraEuler, float fov)>();

        protected override async UniTask Awake()
        {
            _status = Status.Idle;
            await UniTask.CompletedTask;
        }

        public void SetCameraTransformConfig(string sceneName, Vector3 cameraPos, Vector3 cameraEuler, float fov)
        {
            _sceneCameraTransformConfig[sceneName] = (cameraPos, cameraEuler, fov);
        }

#pragma warning disable LOG003 // 禁止使用YooAssets.LoadSceneAsync
#pragma warning disable LOG006 // 禁止直接调用YooAssets类,应改为ResourcesService
        public async UniTask LoadSceneAsync(string sceneName, bool forceLoad = false)
        {
            var hasCameraConfig = _sceneCameraTransformConfig.TryGetValue(sceneName, out var cameraTransform);
            if (_status == Status.Loading)
            {
                return;
            }
            if (_currentSceneName == sceneName && !forceLoad)
            {
                if (hasCameraConfig)
                {
                    var taskRotate = MainCamera.Instance.Camera.transform.DORotate(cameraTransform.cameraEuler, 1f).ToUniTask();
                    var taskMove = MainCamera.Instance.Camera.transform.DOMove(cameraTransform.cameraPos, 1f).ToUniTask();
                    var taskFov = MainCamera.Instance.Camera.DOFieldOfView(cameraTransform.fov, 1f).ToUniTask();
                    await UniTask.WhenAll(taskRotate, taskMove);
                }
                return;
            }


            var handle = YooAssets.LoadSceneAsync(sceneName);
            if (!handle.IsValid)
            {
                GameLog.Error($"LoadSceneAsync {sceneName} failed, msg: {handle.LastError}");
                return;
            }

            _status = Status.Loading;

            var lastScene = _currentSceneName;
            _currentSceneName = sceneName;

            StartChangeSceneEvent.SendMsg(lastScene, sceneName);

            var loadingPanel = SSS.Get<UIService>().OpenPanel<UILoadingPanel>();
            while (!handle.IsDone)
            {
                loadingPanel.SetProgress(handle.Progress);
                await UniTask.Yield();
            }
            await UniTask.DelayFrame(1); //等待一帧，确保场景加载完成
            if (hasCameraConfig)
            {
                MainCamera.Instance.Camera.transform.position = cameraTransform.cameraPos;
                MainCamera.Instance.Camera.transform.rotation = Quaternion.Euler(cameraTransform.cameraEuler);
            }
            else
            {
                MainCamera.Instance.Camera.transform.position = Vector3.zero;
                MainCamera.Instance.Camera.transform.rotation = Quaternion.identity;
            }
            SSS.Get<UIService>().ClosePanel<UILoadingPanel>();

            CompleteChangeSceneEvent.SendMsg(lastScene, sceneName);

            _status = Status.Idle;

        }
#pragma warning restore LOG006 //  禁止直接调用YooAssets类,应改为ResourcesService
#pragma warning restore LOG003 // 禁止使用YooAssets.LoadSceneAsync

        public string GetCurrentSceneName()
        {
            return _currentSceneName;
        }
    }
}