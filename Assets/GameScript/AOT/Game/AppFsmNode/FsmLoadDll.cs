using Cysharp.Threading.Tasks;
using Game.Log;
using System.Reflection;
using UnityEngine;
using YooAsset;

namespace Game
{
    public enum LoadDllMode
    {
        EditorMode,
        AssetMode,
    }
    public class FsmLoadDll : FsmNode
    {
        public async override void OnEnter()
        {
            var assembly = await GetHotfixAssembly();
            var type = assembly.GetType("Game.StartGame");
            var method = type.GetMethod("Start");
            method.Invoke(null, null);
        }

        public override void OnExit()
        {
        }

        public override void OnUpdate()
        {
        }

        private async UniTask<Assembly> GetHotfixAssembly()
        {
            if (AppConst.LoadDllMode == LoadDllMode.EditorMode)
            {
                GameLog.Debug("从编辑器模式加载热更dll");
                return Assembly.Load(AppConst.HotfixAssemblyName);
            }
            else
            {
                GameLog.Debug("从资源模式加载热更dll");
                var handle = YooAssets.LoadAssetAsync(AppConst.HotfixDllName);
                await handle.ToUniTask();
                var bytes = handle.GetAssetObject<TextAsset>().bytes;
                handle.Release();
                return Assembly.Load(bytes);
            }
        }
    }
}
