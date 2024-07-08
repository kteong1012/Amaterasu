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

        public UI2DAttribute(string prefabPath)
        {
            info = new UI2DPanelInfo
            {
                prefabPath = prefabPath
            };
        }
    }
    public class AllowMultiOpenAttribute : System.Attribute
    {
    }
    public struct UI2DPanelInfo
    {
        public string prefabPath;
        public bool allowMultiOpen;
    }

    [GameService(GameServiceDomain.Game)]
    public partial class UIService : GameService
    {
        private GameObject _uiRoot;
        private Camera _uiCamera;
        private Transform _layerRoot;

        public static UIService Instance { get; private set; }

        protected override UniTask Awake()
        {
            Instance = this;

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
            var handle = SSS.ResourceService.LoadAssetSync<GameObject>("UIRoot");
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
