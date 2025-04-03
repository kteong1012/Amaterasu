using Cysharp.Threading.Tasks;
using Game.Log;
using Luban;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using UniFramework.Event;
using UnityEngine;
using UnityEngine.Video;
using YooAsset;

namespace Game
{
    public class LaunchApp : MonoBehaviour
    {
        public LogLevel editorLogLevel = LogLevel.Debug;
        public EPlayMode editorPlayMode = EPlayMode.EditorSimulateMode;
        public AppConfiguration editorAppConfig;

        private void Start()
        {
            DoStart().Forget();
        }

        private async UniTaskVoid DoStart()
        {
            if (AppInfo.IsEditor)
            {
                // 允许后台运行
                Application.runInBackground = true;
            }

            // 初始化Log
            InitGameLog();
            // 初始化鼠标
            InitCursor();
            // 获取AppInfo
            InitAppInfo();
            // 播放启动视频
            //await PlaySplashVideo();
            // 打开补丁窗口
            OpenPatchWindow();
            // 初始化事件系统
            InitUniEvent();
            // 初始化资源模块
            await InitResourceModule();
            // 加载CSharpConfiguration
            LoadCSharpConfiguration();
            // 加载HotUpdate程序集
            LoadDll();
        }

        private void InitCursor()
        {
            GameCursor.Init();
            GameCursor.SetCursorVisible(true);
            GameCursor.SetCursorType(CursorType.Basic_Normal);
        }

        private void InitGameLog()
        {
            GameLog.LogLevel = AppInfo.IsEditor ? editorLogLevel : LogLevel.Debug;
            GameLog.RegisterLogger(UnityConsoleLogger.Instance);
        }

        private void InitAppInfo()
        {
            if (AppInfo.IsEditor)
            {
                AppInfo.AppConfig = editorAppConfig;
            }
            else
            {
                var path = "appConfig";
                var ta = Resources.Load<TextAsset>(path);
                var config = ta == null ? null : JsonUtility.FromJson<AppConfiguration>(ta.text);
                if (config == null)
                {
                    GameLog.Error("version.json not found");
                    return;
                }

                AppInfo.AppConfig = config;
            }
        }

        private async UniTask PlaySplashVideo()
        {
            var go = Resources.Load<GameObject>("SplashWindow/SplashWindow");
            go = Instantiate(go);
            var videoPlayer = go.GetComponentInChildren<VideoPlayer>();
            videoPlayer.Play();
            var clip = videoPlayer.clip;
            var duration = clip.length;
            await UniTask.Delay(TimeSpan.FromSeconds(duration));
            Destroy(go);
        }

        private void OpenPatchWindow()
        {
            var go = Resources.Load<GameObject>("PatchWindow/PatchWindow");
            Instantiate(go);
        }

        private void InitUniEvent()
        {
            UniEvent.Initalize();
        }

        private async UniTask InitResourceModule()
        {
            GameLog.Debug("初始化资源模块");
            YooAssets.Initialize(UnityConsoleLogger.Instance);
            GameLog.Debug("初始化资源模块完成");

            var playMode = AppInfo.IsEditor ? editorPlayMode : AppInfo.AppConfig.ReleasePlayMode;
            AppInfo.PlayMode = playMode;

            // 开始更新主资源补丁
            GameLog.Debug("开始更新主资源补丁");
            PatchOperation operation = new PatchOperation(AppInfo.AppConfig.mainPackageName, EDefaultBuildPipeline.BuiltinBuildPipeline.ToString(), playMode);
            YooAssets.StartOperation(operation);
            await operation.ToUniTask();
            GameLog.Debug("更新主资源补丁完成");
            var gamePackage = YooAssets.GetPackage(AppInfo.AppConfig.mainPackageName);
            YooAssets.SetDefaultPackage(gamePackage);

            if (!string.IsNullOrEmpty(AppInfo.AppConfig.rawFilePackageName))
            {
                // 开始更新原生文件资源补丁
                GameLog.Debug("开始更新原生文件资源补丁");
                operation = new PatchOperation(AppInfo.AppConfig.rawFilePackageName, EDefaultBuildPipeline.RawFileBuildPipeline.ToString(), playMode);
                YooAssets.StartOperation(operation);
                await operation.ToUniTask();
                GameLog.Debug("更新原生文件资源补丁完成");
            }
        }

        private void LoadCSharpConfiguration()
        {
            if (AppInfo.PlayMode == EPlayMode.EditorSimulateMode)
            {
                return;
            }
            var json = LoadRawFileTextSync("csharpconfig");
            var config = JsonUtility.FromJson<CSharpConfiguration>(json);
            AppInfo.CSharpConfig = config;
        }

        private void LoadDll()
        {
            PatchEventDefine.PatchStatesChange.SendEventMessage("加载逻辑资源");

            var entryAssembly = (Assembly)null;

            if (AppInfo.IsEditor)
            {
                entryAssembly = AppDomain.CurrentDomain.GetAssemblies().First(a => a.GetName().Name == "Game.HotUpdate");
                TypeManager.Instance.AddAssembly(entryAssembly);
            }
            else
            {
                GameLog.Debug("加载HotUpdate程序集");

                // 补充Aot泛型元数据程序集
                LoadAotGenericMetadataAssemblies();

                // 加载HotUpdate程序集
                foreach (var assName in AppInfo.CSharpConfig.hotupdateAssemblies)
                {
                    var location = $"{assName}.dll";
                    var dllBytes = LoadRawFileDataSync(location);
                    GameLog.Debug($"加载HotUpdate程序集: {location}, {dllBytes.Length} bytes");
                    var ass = Assembly.Load(dllBytes);
                    TypeManager.Instance.AddAssembly(ass);
                    if (assName == "Game.HotUpdate")
                    {
                        entryAssembly = ass;
                    }
                }
            }
            if (entryAssembly == null)
            {
                GameLog.Error("加载不到逻辑程序集");
            }
            // 调用HotUpdate程序集的StartGame.Start方法
            var typeStartGame = entryAssembly.GetType("Game.StartGame");
            var methodStart = typeStartGame.GetMethod("Start", BindingFlags.Static | BindingFlags.Public);
            methodStart.Invoke(null, null);
        }

        private void LoadAotGenericMetadataAssemblies()
        {
            var resourcePath = "AotDlls/AotPatchAssemblies";
            var ta = Resources.Load<TextAsset>(resourcePath);
            if (ta == null)
            {
                GameLog.Warning($"{resourcePath} 找不到，文件缺失或者不需要补充泛型元数据");
                return;
            }
            var buf = new ByteBuf(ta.bytes);

            // read count
            var assemblyCount = buf.ReadSize();

            for (var i = 0; i < assemblyCount; i++)
            {
                // read name
                var dllName = buf.ReadString();
                // read bytes
                var bytes = buf.ReadBytes();
                var ret = HybridCLR.RuntimeApi.LoadMetadataForAOTAssembly(bytes, HybridCLR.HomologousImageMode.SuperSet);
                if (ret == HybridCLR.LoadImageErrorCode.OK)
                {
                    GameLog.Info($"加载AOT元数据成功: {dllName}");
                }
                else
                {
                    GameLog.Error($"加载AOT元数据失败: {dllName}. {ret}");
                }
            }
        }

        private static byte[] LoadRawFileDataSync(string location)
        {
            var rawPackage = YooAssets.GetPackage(AppInfo.AppConfig.rawFilePackageName);
            var handle = rawPackage.LoadRawFileSync(location);
            var dllBytes = handle.GetRawFileData();
            handle.Release();
            return dllBytes;
        }

        private static string LoadRawFileTextSync(string location)
        {
            var rawPackage = YooAssets.GetPackage(AppInfo.AppConfig.rawFilePackageName);
            var handle = rawPackage.LoadRawFileSync(location);
            var text = handle.GetRawFileText();
            handle.Release();
            return text;
        }
    }
}
