using Game.Log;

namespace Game
{
    public static class AppInfo
    {
        public static Version AppVersion { get; private set; }

        private static AppConfiguration _appconfig;
        public static AppConfiguration AppConfig
        {
            get => _appconfig;
            set
            {
                AppVersion = new Version(value.appVersion);
                GameLog.Info($"AppVersion: {AppVersion}");
                _appconfig = value;
            }
        }

        public static Version CSharpVersion { get; private set; }
        private static CSharpConfiguration _csharpConfig;
        public static CSharpConfiguration CSharpConfig
        {
            get => _csharpConfig;
            set
            {
                CSharpVersion = new Version(value.csharpVersion);
                GameLog.Info($"CSharpVersion: {CSharpVersion}");
                _csharpConfig = value;
            }
        }
    }
}
