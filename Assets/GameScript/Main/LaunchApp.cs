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
            // �򿪲�������
            OpenPatchWindow();
            // ����App����
            SetApp();
            // ��ʼ��Log
            SetGameLog();
            // ��ʼ���¼�ϵͳ
            InitUniEvent();
            // ��ʼ����Դģ��
            await InitResourceModule();
            // ����HotUpdate����
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

            // ��ʼ������������
            PatchOperation operation = new PatchOperation(AppSettings.YooAssetPackageName, EDefaultBuildPipeline.BuiltinBuildPipeline.ToString(), AppSettings.PlayMode);
            YooAssets.StartOperation(operation);

            await operation.ToUniTask();

            // ����Ĭ�ϵ���Դ��
            var gamePackage = YooAssets.GetPackage(AppSettings.YooAssetPackageName);
            YooAssets.SetDefaultPackage(gamePackage);
        }

        private void LoadDll()
        {
            PatchEventDefine.PatchStatesChange.SendEventMessage("�����߼���Դ");
            // ����HotUpdate����
#if UNITY_EDITOR

            // ����Aot����Ԫ����
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
                    GameLog.Debug($"����AOTԪ���ݳɹ�: {dllName}");
                }
                else
                {
                    GameLog.Error($"����AOTԪ����ʧ��: {dllName}. {ret}");
                }
            }

            var dllPath = AppSettings.HotUpdateDllAssetPath;
            var handle = YooAssets.LoadAssetSync<TextAsset>(dllPath);
            var dllBytes = handle.GetAssetObject<TextAsset>().bytes;
            Assembly hotUpdateAss = Assembly.Load(dllBytes);
            handle.Release();
#else
            // Editor��������أ�ֱ�Ӳ��һ��HotUpdate����
            GameLog.Debug("Editor�������������HotUpdate����");
            Assembly hotUpdateAss = AppDomain.CurrentDomain.GetAssemblies().First(a => a.GetName().Name == "HotUpdate");
#endif
            // ����HotUpdate���򼯵�StartGame.Start����
            var typeStartGame = hotUpdateAss.GetType("Game.StartGame");
            var methodStart = typeStartGame.GetMethod("Start", BindingFlags.Static | BindingFlags.Public);
            methodStart.Invoke(null, null);
        }
    }
}
