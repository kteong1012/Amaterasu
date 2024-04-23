using Cysharp.Threading.Tasks;
using Game;
using Game.Log;
using I2.Loc;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using YIUIFramework;
using YooAsset;

namespace Game
{
    [GameService(GameServiceLifeSpan.Game)]
    public class UIService : GameService
    {
        #region Fields & Properties
        private Dictionary<int, AssetHandle> _allHandle = new();
        #endregion

        #region Life Cycle
        protected override async UniTask Awake()
        {
            UIBindHelper.InternalGameGetUIBindVoFunc = YIUICodeGenerated.UIBindProvider.Get;
            YIUILoadDI.LoadAssetFunc = LoadAsset;
            YIUILoadDI.LoadAssetAsyncFunc = LoadAssetAsync;
            YIUILoadDI.ReleaseAction = ReleaseAction;

            await MgrCenter.Inst.Register(SemaphoreSlimSingleton.Inst);
            await MgrCenter.Inst.Register(CountDownMgr.Inst);
            await MgrCenter.Inst.Register(RedDotMgr.Inst);
            await MgrCenter.Inst.Register(PanelMgr.Inst);

            //URP相关设置
            var mainURPCamera = MainCamera.Instance.Camera.GetUniversalAdditionalCameraData();
            var uiURPCamera = PanelMgr.Inst.UICamera.GetUniversalAdditionalCameraData();
            mainURPCamera.renderType = CameraRenderType.Base;
            uiURPCamera.renderType = CameraRenderType.Overlay;
            mainURPCamera.cameraStack.Add(PanelMgr.Inst.UICamera);
        }
        #endregion

        #region Private Methods
        private (UnityEngine.Object, int) LoadAssetHandle(AssetHandle handle)
        {
            if (handle.AssetObject != null)
            {
                var hashCode = handle.GetHashCode();
                _allHandle.Add(hashCode, handle);
                return (handle.AssetObject, hashCode);
            }
            return (null, 0);
        }
        #endregion

        #region Delegate Methods
        private (UnityEngine.Object, int) LoadAsset(string packageName, string assetName, Type objectType)
        {
            var handle = YooAssets.LoadAssetSync(assetName, objectType);
            return LoadAssetHandle(handle);
        }

        private async UniTask<(UnityEngine.Object, int)> LoadAssetAsync(string packageName, string assetName, Type objectType)
        {
            var handle = YooAssets.LoadAssetAsync(assetName, objectType);
            await handle.ToUniTask();
            return LoadAssetHandle(handle);
        }

        private void ReleaseAction(int hashCode)
        {
            if (_allHandle.TryGetValue(hashCode, out var handle))
            {
                handle.Release();
                _allHandle.Remove(hashCode);
            }
            else
            {
                GameLog.Error("试图释放一个不存在的资源");
            }
        }
        #endregion
    }
}