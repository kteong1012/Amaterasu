using Cysharp.Threading.Tasks;
using Game.Log;
using System;
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
    public class FsmGameLoop : FsmNode
    {
        private Action _gameLoopUpdateDelegate;
        private Action _gameLoopReleaseDelegate;

        public async override void OnEnter()
        {
            var assembly = await GetHotfixAssembly();
            var type = assembly.GetType("Game.GameLoop");
            var methodStart = type.GetMethod("Start");
            _gameLoopUpdateDelegate = type.GetMethod("Update").CreateDelegate(typeof(Action)) as Action;
            _gameLoopReleaseDelegate = type.GetMethod("Release").CreateDelegate(typeof(Action)) as Action;

            methodStart.Invoke(null, null);
        }

        public override void OnExit()
        {
            _gameLoopReleaseDelegate?.Invoke();
        }

        public override void OnUpdate()
        {
            _gameLoopUpdateDelegate?.Invoke();
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
