using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using UniFramework.Event;
using UnityEngine;
using YooAsset;

namespace Game
{

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
            _eventGroup.AddListener<UIEventDefine.UI2DNodeReleaseEvent>(OnUI2DNodeReleaseEvent);

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
            UpdatePanelPool();
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

        private void OnUI2DNodeReleaseEvent(IEventMessage message)
        {
            var msg = message as UIEventDefine.UI2DNodeReleaseEvent;
            if (msg == null)
            {
                return;
            }
            if (msg.Node != null)
            {
                RecycleNode(msg.Node);
            }
        }
    }
}
