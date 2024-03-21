using YooAsset;

namespace Game
{
    public static class AppConst
    {
        //Resource
        public static EPlayMode PlayMode = EPlayMode.HostPlayMode;

        //Hotfix
        public static LoadDllMode LoadDllMode = LoadDllMode.AssetMode;
        public static string HotfixAssemblyName = "Hotfix";
        public static string HotfixDllName => $"{HotfixAssemblyName}.dll";
    }
}
