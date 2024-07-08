using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using UniFramework.Event;
using UnityEngine;
using YooAsset;

namespace Game
{

    [System.Flags]
    public enum UI2DPanelOptions
    {
        None = 0,
        NotPlayOpenAnim = 1,
        NotPlayCloseAnim = 2,
        NotPlayAnim = NotPlayOpenAnim | NotPlayCloseAnim,
        AllowMultiOpen = 4,
    }

    public class UI2DAttribute : System.Attribute
    {
        public UI2DPanelInfo info;

        public UI2DAttribute(string prefabPath, bool allowMultiOpen = false)
        {
            info = new UI2DPanelInfo
            {
                prefabPath = prefabPath,
                allowMultiOpen = allowMultiOpen
            };
        }
    }
    public class UI2DPanelInfo
    {
        public string prefabPath;
        public bool allowMultiOpen;
    }

    [GameService(GameServiceDomain.Game)]
    public partial class UIService : GameService
    {
        private Dictionary<string, UI2DPanelInfo> ui2DPanelInfos = new Dictionary<string, UI2DPanelInfo>();
        private GameObject _uiRoot;
        private Camera _uiCamera;
        private Transform _layerRoot;

        public static UIService Instance { get; private set; }

        protected override UniTask Awake()
        {
            Instance = this;
            var types = TypeManager.Instance.GetTypes();
            foreach (var type in types)
            {
                var attr = type.GetCustomAttributes(typeof(UI2DAttribute), false);
                if (attr.Length > 0)
                {
                    var ui2DPanelAttribute = attr[0] as UI2DAttribute;
                    var typeName = type.Name;
                    ui2DPanelInfos.Add(typeName, ui2DPanelAttribute.info);
                }
            }

            _eventGroup.AddListener<UIEventDefine.UI2DPanelCloseEvent>(OnUI2DPanelCloseEvent);

            InitRoot();
            InitLayer();
            return base.Awake();
        }

        protected override void OnDestroy()
        {
            Instance = null;
        }

        public override void Update()
        {
            UpdatePool();
        }

        private void InitRoot()
        {
            var handle = GameServices.ResourceService.LoadAssetSync<GameObject>("UIRoot");
            _uiRoot = handle.InstantiateSync();
            _uiCamera = _uiRoot.GetComponentInChildren<Camera>();
            _layerRoot = _uiRoot.transform.Find("CanvasRoot/UILayerRoot");
            handle.Release();

            UnityEngine.Object.DontDestroyOnLoad(_uiRoot);
             MainCamera.Instance.AddCameraStack(_uiCamera);
        }

        private void OnUI2DPanelCloseEvent(IEventMessage message)
        {
            var msg = message as UIEventDefine.UI2DPanelCloseEvent;
            if (msg == null)
            {
                return;
            }
            ClosePanel(msg.Panel);
        }
    }
}
