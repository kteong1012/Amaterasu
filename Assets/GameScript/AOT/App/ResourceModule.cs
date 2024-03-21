using Cysharp.Threading.Tasks;
using Game.Log;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UniFramework.Event;
using UnityEngine;
using YooAsset;

namespace Game
{
    public class ResourceModule : GameModule
    {
        private const float UNLOAD_UNUSED_ASSETS_INTERVAL = 15f;
        private float _unloadUnusedAssetsCountdown = UNLOAD_UNUSED_ASSETS_INTERVAL;
        private ResourcePackage _gamePackage;

        #region 生命周期
        protected override async UniTask OnInitialize()
        {
            // 初始化事件系统
            UniEvent.Initalize();

            // 初始化资源系统
            YooAssets.Initialize(UnityConsoleLog.Instance);

            // 加载更新页面
            var go = Resources.Load<GameObject>("PatchWindow");
            GameObject.Instantiate(go);

            // 开始补丁更新流程
            PatchOperation operation = new PatchOperation("DefaultPackage", EDefaultBuildPipeline.BuiltinBuildPipeline.ToString(), AppConst.PlayMode);
            YooAssets.StartOperation(operation);

            await operation.ToUniTask();

            // 设置默认的资源包
            _gamePackage = YooAssets.GetPackage("DefaultPackage");
            YooAssets.SetDefaultPackage(_gamePackage);
        }

        private void Update()
        {
            if (_gamePackage == null)
            {
                return;
            }
            // 定时卸载未使用的资源
            if (_unloadUnusedAssetsCountdown > 0)
            {
                _unloadUnusedAssetsCountdown -= Time.deltaTime;
            }
            else
            {
                _gamePackage.UnloadUnusedAssets();
                _unloadUnusedAssetsCountdown += UNLOAD_UNUSED_ASSETS_INTERVAL;
            }
        }
        #endregion
    }
}
