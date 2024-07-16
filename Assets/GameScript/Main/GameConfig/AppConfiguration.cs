using System;
using YooAsset;

namespace Game
{
    [Serializable]
    public class AppConfiguration
    {
        public string appVersion = Version.DefaultVersion;
        public bool isDevelopmentMode;
        public bool enableHotupdate;
        public string cdnHostUrl;
        public string mainPackageName;
        public string rawFilePackageName;

        public EPlayMode ReleasePlayMode => enableHotupdate ? EPlayMode.HostPlayMode : EPlayMode.OfflinePlayMode;

    }
}
