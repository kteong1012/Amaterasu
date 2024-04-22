using Cysharp.Threading.Tasks;
using Game.Log;
using Sirenix.Serialization;
using System;
using System.Linq;
using System.Reflection;
using UniFramework.Event;
using UnityEngine;
using YooAsset;

namespace Game
{
    public class LaunchApp : MonoBehaviour
    {
        public EPlayMode PlayMode = EPlayMode.EditorSimulateMode;

        private async void Start()
        {
            // 打开补丁窗口
            OpenPatchWindow();
            // 设置App常量
            SetApp();
            // 初始化Log
            SetGameLog();
            // 初始化事件系统
            InitUniEvent();
            // 初始化资源模块
            await InitResourceModule();
            // 加载HotUpdate程序集
            LoadDll();
        }

        private void OpenPatchWindow()
        {
            var go = Resources.Load<GameObject>("PatchWindow/PatchWindow");
            GameObject.Instantiate(go);
        }

        private void SetApp()
        {
            var isEditor = Application.isEditor;
            AppSettings.PlayMode = isEditor ? PlayMode : EPlayMode.HostPlayMode;
        }

        private static void InitUniEvent()
        {
            UniEvent.Initalize();
        }

        private static void SetGameLog()
        {
            GameLog.RegisterLogger(UnityConsoleLog.Instance);
        }

        private static async UniTask InitResourceModule()
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

        private void LoadDll()
        {
            PatchEventDefine.PatchStatesChange.SendEventMessage("加载逻辑资源");
            // 加载HotUpdate程序集
#if !UNITY_EDITOR

            // 补充Aot泛型元数据
            var aotDllNames = AOTGenericReferences.PatchedAOTAssemblyList;
            foreach (var dllName in aotDllNames)
            {
                var ta = Resources.Load<TextAsset>($"AotDlls/{dllName}");
                if (ta == null)
                {
                    GameLog.Error($"AotDlls/{dllName} not found");
                    continue;
                }
                var bytes = ta.bytes;
                var ret = HybridCLR.RuntimeApi.LoadMetadataForAOTAssembly(bytes, HybridCLR.HomologousImageMode.SuperSet);
                if (ret == HybridCLR.LoadImageErrorCode.OK)
                {
                    GameLog.Debug($"加载AOT元数据成功: {dllName}");
                }
                else
                {
                    GameLog.Error($"加载AOT元数据失败: {dllName}. {ret}");
                }
            }

            var dllPath = AppSettings.HotUpdateDllAssetPath;
            var handle = YooAssets.LoadAssetSync<TextAsset>(dllPath);
            var dllBytes = handle.GetAssetObject<TextAsset>().bytes;
            GameLog.Debug($"加载HotUpdate程序集: {dllPath}, {dllBytes.Length} bytes");
            Assembly hotUpdateAss = Assembly.Load(dllBytes);
            handle.Release();
#else
            // Editor下无需加载，直接查找获得HotUpdate程序集
            GameLog.Debug("Editor环境下无需加载HotUpdate程序集");
            Assembly hotUpdateAss = AppDomain.CurrentDomain.GetAssemblies().First(a => a.GetName().Name == "HotUpdate");
#endif
            // 调用HotUpdate程序集的StartGame.Start方法
            var typeStartGame = hotUpdateAss.GetType("Game.StartGame");
            var methodStart = typeStartGame.GetMethod("Start", BindingFlags.Static | BindingFlags.Public);
            methodStart.Invoke(null, null);
        }
    }
}
