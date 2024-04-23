
using Cysharp.Threading.Tasks;
using Game.Log;
using UniFramework.Event;
using UnityEngine;
using YooAsset;

namespace Game
{
    [GameService(GameServiceDomain.Game)]
    public class ResourceService : GameService
    {
        private const float UNLOAD_UNUSED_ASSETS_INTERVAL = 15f;
        private float _unloadUnusedAssetsCountdown = UNLOAD_UNUSED_ASSETS_INTERVAL;
        private ResourcePackage _gamePackage;

        public override void Update()
        {
            if (_gamePackage == null)
            {
                _gamePackage = YooAssets.GetPackage(AppSettings.YooAssetPackageName);
                if (_gamePackage == null)
                {
                    return;
                }
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
                GameLog.Debug("== UnloadUnusedAssets ==");
            }
        }
    }
}
