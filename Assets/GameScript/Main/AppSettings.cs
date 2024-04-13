using UnityEngine;
using YooAsset;

namespace Game
{
    public class AppSettings
    {
        //  Common
        public static string PlatformName
        {
            get
            {
                switch (Application.platform)
                {
                    case RuntimePlatform.Android:
                        return "Android";
                    case RuntimePlatform.IPhonePlayer:
                        return "iOS";
                    case RuntimePlatform.WindowsPlayer:
                    case RuntimePlatform.WindowsEditor:
                        return "Windows";
                    case RuntimePlatform.OSXPlayer:
                    case RuntimePlatform.OSXEditor:
                        return "OSX";
                    default:
                        throw new System.NotImplementedException();
                }
            }
        }

        public static string PersistentAssetsSourcePath => $"{Application.persistentDataPath}";
        public static string StreamingAssetsSourcePath => $"{Application.streamingAssetsPath}";

        //  HotUpdate
        public static string HotUpdateAssemblyName => "HotUpdate";
        public static string HotUpdateDllName => $"{HotUpdateAssemblyName}.dll";
        private static string _dllPath => $"Dlls/{HotUpdateDllName}.bytes";
        public static string PersistentDllPath => $"{PersistentAssetsSourcePath}/{_dllPath}";
        public static string StreamingDllPath => $"{StreamingAssetsSourcePath}/{PlatformName}/{_dllPath}";
        public static string HotUpdateDllPath => System.IO.File.Exists(PersistentDllPath) ? PersistentDllPath : StreamingDllPath;


        //  YooAssets
        public static EPlayMode PlayMode { get; internal set; }
        public static string YooAssetPackageName => "DefaultPackage";

    }
}
