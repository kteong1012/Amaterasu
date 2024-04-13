using System.Collections.Generic;
using UnityEngine;
using UniFramework.Event;
using Game.Log;
using System;
using YooAsset;
using Cysharp.Threading.Tasks;
using System.Threading.Tasks;

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

            // 初始化Log
            SetGameLog();

            // 初始化事件系统
            InitUniEvent();

            // 添加事件监听
            AddEventListeners();

            // 初始化资源系统
            await InitResourceModule();

            // 初始化服务
            InitServiceManager();

            // 创建Game服务
            await _serviceManager.StartServices(GameServiceLifeSpan.Game);

            // 进入登录界面
            _ = YooAssets.LoadSceneAsync("scene_home");
        }

        private void AddEventListeners()
        {
        }

        private static async Task InitResourceModule()
        {
            YooAssets.Initialize(UnityConsoleLog.Instance);

            // 开始补丁更新流程
            PatchOperation operation = new PatchOperation(AppSettings.YooAssetPackageName, EDefaultBuildPipeline.BuiltinBuildPipeline.ToString(), AppSettings.PlayMode);
            YooAssets.StartOperation(operation);

            await operation.ToUniTask();

            // 设置默认的资源包
            var gamePackage = YooAssets.GetPackage(AppSettings.YooAssetPackageName);
            YooAssets.SetDefaultPackage(gamePackage);
        }

        private void InitServiceManager()
        {
            _serviceManager = new GameServiceManager();
        }

        public async UniTask StartServices(GameServiceLifeSpan lifeSpan)
        {
            await _serviceManager.StartServices(lifeSpan);
        }

        public void StopServices(GameServiceLifeSpan lifeSpan)
        {
            _serviceManager.StopServices(lifeSpan);
        }

        private static void InitUniEvent()
        {
            UniEvent.Initalize();
        }

        private static void SetGameLog()
        {
            GameLog.RegisterLogger(UnityConsoleLog.Instance);
        }

        private void Update()
        {
            _serviceManager?.Update();
        }

        private void OnDestroy()
        {
            _serviceManager?.StopServices(GameServiceLifeSpan.Game);
            _serviceManager?.Release();

            _eventGroup?.RemoveAllListener();

            GameLog.ClearLogger();
        }
    }
}