using Codice.Utils;
using Cysharp.Threading.Tasks;
using Game.Log;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using UniFramework.Event;
using UnityEngine;
using YooAsset;

namespace Game
{
    public class LaunchApp : MonoBehaviour
    {
        private async void Start()
        {
            // 打开补丁窗口
            OpenPatchWindow();
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
            if (!GameConfig.Instance.HotupdateConfig.EnableHotupdate)
            {
                return;
            }
            var go = Resources.Load<GameObject>("PatchWindow/PatchWindow");
            GameObject.Instantiate(go);
        }

        private static void InitUniEvent()
        {
            UniEvent.Initalize();
        }

        private static void SetGameLog()
        {
            GameLog.RegisterLogger(UnityConsoleLogger.Instance);
        }

        private static async UniTask InitResourceModule()
        {
            GameLog.Debug("初始化资源模块");
            YooAssets.Initialize(UnityConsoleLogger.Instance);
            GameLog.Debug("初始化资源模块完成");

            // 开始补丁更新流程
            PatchOperation operation = new PatchOperation(GameConfig.Instance.HotupdateConfig.PackageName, EDefaultBuildPipeline.BuiltinBuildPipeline.ToString(), GameConfig.Instance.HotupdateConfig.PlayMode);
            YooAssets.StartOperation(operation);

            await operation.ToUniTask();

            // 设置默认的资源包
            var gamePackage = YooAssets.GetPackage(GameConfig.Instance.HotupdateConfig.PackageName);
            YooAssets.SetDefaultPackage(gamePackage);
        }

        private void LoadDll()
        {
            PatchEventDefine.PatchStatesChange.SendEventMessage("加载逻辑资源");
            // 加载HotUpdate程序集

            if (!GameConfig.Instance.HotupdateConfig.EnableHotupdate)
            {
                // 未开启热更新，直接加载HotUpdate程序集
                GameLog.Debug("未开启热更新，直接加载HotUpdate程序集");
            }
            else if (Application.isEditor)
            {
                // Editor下无需加载，直接查找获得HotUpdate程序集
                GameLog.Debug("Editor环境下无需加载HotUpdate程序集");
            }
            else
            {
                GameLog.Debug("加载HotUpdate程序集");

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
                // 加载HotUpdate程序集
                foreach (var dllName in GameConfig.Instance.HotupdateConfig.HotupdateAssemblies)
                {
                    var dllPath = Path.Combine(GameConfig.Instance.HotupdateConfig.HotUpdateDllAssetFolder, dllName);
                    var handle = YooAssets.LoadAssetSync<TextAsset>(dllPath);
                    var dllBytes = handle.GetAssetObject<TextAsset>().bytes;
                    GameLog.Debug($"加载HotUpdate程序集: {dllPath}, {dllBytes.Length} bytes");
                    Assembly.Load(dllBytes);
                    handle.Release();
                }
            }

            var hotUpdateAss = AppDomain.CurrentDomain.GetAssemblies().First(a => a.GetName().Name == "HotUpdate");
            // 调用HotUpdate程序集的StartGame.Start方法
            var typeStartGame = hotUpdateAss.GetType("Game.StartGame");
            var methodStart = typeStartGame.GetMethod("Start", BindingFlags.Static | BindingFlags.Public);
            methodStart.Invoke(null, null);
        }
    }
}
