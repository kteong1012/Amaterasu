using System.Collections.Generic;
using UnityEngine;
using UniFramework.Event;
using Game.Log;
using System;
using YooAsset;
using Cysharp.Threading.Tasks;
using System.Threading.Tasks;
using YIUIFramework;
using Game.UI.UILogin;

namespace Game
{
    public class GameEntry : MonoBehaviour
    {
        public static GameEntry Ins { get; private set; }

        private readonly EventGroup _eventGroup = new EventGroup();

        private GameServiceManager _serviceManager;

        private async void Awake()
        {
            Ins = this;
            Application.targetFrameRate = 60;
            Application.runInBackground = true;
            DontDestroyOnLoad(this.gameObject);

            // 添加事件监听
            AddEventListeners();

            // 关闭更新窗口
            PatchEventDefine.ClosePatchWindow.SendEventMessage();

            // 初始化服务管理器
            InitServiceManager();

            // 启动Game服务
            await _serviceManager.StartServices(GameServiceDomain.Game);

            // 进入登录场景
            var sceneService = GetService<SceneService>();
            sceneService.ChangeToLoginScene().Forget();
        }

        private void AddEventListeners()
        {
        }

        private void InitServiceManager()
        {
            _serviceManager = new GameServiceManager();
        }

        public async UniTask StartServices(GameServiceDomain domain)
        {
            await _serviceManager.StartServices(domain);
        }

        public void StopServices(GameServiceDomain domain)
        {
            _serviceManager.StopServices(domain);
        }

        private void Update()
        {
            _serviceManager?.Update();
        }

        private void OnDestroy()
        {
            _eventGroup?.RemoveAllListener();
            YooAssets.Destroy();
            GameLog.ClearLogger();
        }
        private void OnApplicationQuit()
        {
            _serviceManager?.Release();
        }

        public T GetService<T>() where T : GameService
        {
            return _serviceManager.GetService<T>();
        }
    }
}