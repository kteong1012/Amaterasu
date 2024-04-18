using System;
using System.Linq;
using System.Reflection;
using UnityEngine;
using YooAsset;

namespace Game
{
    public class LaunchApp : MonoBehaviour
    {
        public EPlayMode PlayMode = EPlayMode.EditorSimulateMode;

        private void Start()
        {
            OpenPatchWindow();
            SetApp();
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

        private void LoadDll()
        {
            PatchEventDefine.PatchStatesChange.SendEventMessage("加载HotUpdate程序集");
            // Editor环境下，HotUpdate.dll.bytes已经被自动加载，不需要加载，重复加载反而会出问题。
#if !UNITY_EDITOR
            var dllPath = AppSettings.HotUpdateDllPath;
            var dllBytes = System.IO.File.ReadAllBytes(dllPath);
            Assembly hotUpdateAss = Assembly.Load(dllBytes);
#else
            // Editor下无需加载，直接查找获得HotUpdate程序集
            Assembly hotUpdateAss = AppDomain.CurrentDomain.GetAssemblies().First(a => a.GetName().Name == "HotUpdate");
#endif
            var typeStartGame = hotUpdateAss.GetType("Game.StartGame");
            var methodStart = typeStartGame.GetMethod("Start", BindingFlags.Static | BindingFlags.Public);
            methodStart.Invoke(null, null);
        }
    }
}
