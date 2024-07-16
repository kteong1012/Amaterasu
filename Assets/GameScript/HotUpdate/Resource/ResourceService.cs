
using Cysharp.Threading.Tasks;
using Game.Log;
using UniFramework.Event;
using UnityEngine;
using YooAsset;

namespace Game
{
    [GameService(GameServiceDomain.Game)]
    public partial class ResourceService : GameService
    {
        private const float UNLOAD_UNUSED_ASSETS_INTERVAL = 15f;
        private float _unloadUnusedAssetsCountdown = UNLOAD_UNUSED_ASSETS_INTERVAL;
        private ResourcePackage _mainPackage;
        private ResourcePackage _rawFilePackage;

        protected override UniTask Awake()
        {
#pragma warning disable LOG006 //  禁止直接调用YooAssets类,应改为ResourcesService
            _mainPackage = YooAssets.GetPackage(AppInfo.AppConfig.mainPackageName);
            _rawFilePackage = YooAssets.GetPackage(AppInfo.AppConfig.rawFilePackageName);
#pragma warning restore LOG006 //  禁止直接调用YooAssets类,应改为ResourcesService
            return UniTask.CompletedTask;
        }

        public override void Update()
        {
            // 定时卸载未使用的资源
            if (_unloadUnusedAssetsCountdown > 0)
            {
                _unloadUnusedAssetsCountdown -= Time.deltaTime;
            }
            else
            {
                _mainPackage.UnloadUnusedAssetsAsync();
                _unloadUnusedAssetsCountdown += UNLOAD_UNUSED_ASSETS_INTERVAL;
            }
        }
    }
}
