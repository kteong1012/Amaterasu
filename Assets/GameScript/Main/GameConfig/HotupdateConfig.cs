using UnityEngine;
using YooAsset;

namespace Game
{
    [System.Serializable]
    public class HotupdateConfig
    {
        public bool EnableHotupdate = true;
        [SerializeField]
        private EPlayMode _playMode = EPlayMode.EditorSimulateMode;
        public EPlayMode PlayMode
        {
            get
            {
#if !UNITY_EDITOR
                _playMode = GetReleasePlayMode();
#endif
                return _playMode;
            }
        }
        [SerializeField]
        private string _packageName = "DefaultPackage";
        public string PackageName => _packageName;

        [SerializeField]
        private string _hotUpdateDllAssetFolder => $"Assets/GameRes/Dlls";
        public string HotUpdateDllAssetFolder => _hotUpdateDllAssetFolder;

        [SerializeField]
        private string _cdnHostUrl = "http://localhost:8080";
        public string CdnHostUrl => _cdnHostUrl;

        [SerializeField]
        private string[] _hotupdateAssemblies = new string[] { "HotUpdate.dll" };
        public string[] HotupdateAssemblies => _hotupdateAssemblies;


        private EPlayMode GetReleasePlayMode()
        {
            return EnableHotupdate ? EPlayMode.HostPlayMode : EPlayMode.OfflinePlayMode;
        }
    }
}
