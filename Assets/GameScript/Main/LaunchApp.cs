using Cysharp.Threading.Tasks;
using Game.Log;
using System;
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

        private async void Start()
        {
            // 初始化Log
            InitGameLog();
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

        private void InitGameLog()
        {

#if UNITY_EDITOR
            GameLog.LogLevel = editorLogLevel;
#else
            GameLog.LogLevel = LogLevel.Debug;
#endif
            GameLog.RegisterLogger(UnityConsoleLogger.Instance);
        }

        private void InitAppInfo()
        {
            // 读取AppConfig
            {
                var path = "appConfig";
                var ta = Resources.Load<TextAsset>(path);
                var config = ta == null ? null : JsonUtility.FromJson<AppConfiguration>(ta.text);
#if UNITY_EDITOR
                if (config == null)
                {
                    // 编辑器模式下，如果没有version.config文件，创建一份
                    config = new AppConfiguration();
                    var json = JsonUtility.ToJson(config);
                    File.WriteAllText("Assets/Resources/appConfig.json", json);
                    UnityEditor.AssetDatabase.Refresh();
                }
#endif
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
            if (!AppInfo.AppConfig.enableHotupdate)
            {
                return;
            }
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

            var playMode = Application.isEditor ? editorPlayMode : AppInfo.AppConfig.ReleasePlayMode;

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
            }
        }

        private void LoadCSharpConfiguration()
        {
            if (Application.isEditor)
            {
                return;
            }
            var rawPackage = YooAssets.GetPackage(AppInfo.AppConfig.rawFilePackageName);
            var handle = rawPackage.LoadRawFileSync("csharpconfig");
            if (handle == null)
            {
                GameLog.Error("加载不到CSharpConfiguration");
                return;
            }
            var json = handle.GetRawFileText();
            var config = JsonUtility.FromJson<CSharpConfiguration>(json);
            AppInfo.CSharpConfig = config;
        }

        private void LoadDll()
        {
            PatchEventDefine.PatchStatesChange.SendEventMessage("加载逻辑资源");
            // 加载HotUpdate程序集

            var entryAssembly = (Assembly)null;
            if (Application.isEditor)
            {
                entryAssembly = AppDomain.CurrentDomain.GetAssemblies().First(a => a.GetName().Name == "HotUpdate");
            }
            else
            {
                GameLog.Debug("加载HotUpdate程序集");

                // 补充Aot泛型元数据
                var aotDllNames = AOTGenericReferences.PatchedAOTAssemblyList.Concat(AppInfo.AppConfig.aotMetaDlls).Distinct();
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
                foreach (var assName in AppInfo.CSharpConfig.hotupdateAssemblies)
                {
                    var location = $"{assName}.dll";
                    var handle = YooAssets.LoadRawFileSync(location);
                    var dllBytes = handle.GetRawFileData();
                    GameLog.Debug($"加载HotUpdate程序集: {location}, {dllBytes.Length} bytes");
                    var ass = Assembly.Load(dllBytes);
                    if (assName == "HotUpdate")
                    {
                        entryAssembly = ass;
                    }
                    handle.Release();
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
    }
}
