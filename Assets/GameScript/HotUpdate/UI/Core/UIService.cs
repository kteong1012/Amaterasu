using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using Game.Cfg;
using UniFramework.Event;
using UnityEngine;
using YooAsset;

namespace Game
{
    [GameService(GameServiceDomain.Game)]
    public partial class UIService : GameService
    {
        private UIRoot _uiRoot;
        private Camera _UiCamera => _uiRoot.uiCamera;
        private Transform _LayerRoot => _uiRoot.layerRoot;
        private Transform _DragLayer => _uiRoot.dragLayer;

        protected override UniTask Awake()
        {
            _eventGroup.AddListener<UIEventDefine.UI2DPanelCloseEvent>(OnUI2DPanelCloseEvent);
            _eventGroup.AddListener<UIEventDefine.UI2DNodeReleaseEvent>(OnUI2DNodeReleaseEvent);
            _eventGroup.AddListener<SwitchLanguageEvent>(OnSwitchLanguageEvent);
            return base.Awake();
        }

        protected override UniTask Start()
        {
            InitRoot();
            InitLayer();
            return base.Start();
        }

        public override void Update()
        {
            UpdateDrag();//大盘改的，程序记得看下对不对
            UpdatePanelPool();
        }

        public override void LateUpdate()
        {
           
        }

        private void InitRoot()
        {
            var handle = SSS.Get<ResourceService>().LoadAssetSync<GameObject>("UIRoot");
            var gob = handle.InstantiateSync();
            _uiRoot = gob.GetComponent<UIRoot>();
            handle.Release();

            UnityEngine.Object.DontDestroyOnLoad(_uiRoot.gameObject);
            MainCamera.Instance.AddCameraStack(_uiRoot.uiCamera);
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

            if (msg.Node)
            {
                RecycleNode(msg.Node);
            }
        }

        private void OnSwitchLanguageEvent(IEventMessage message)
        {
            var msg = message as SwitchLanguageEvent;
            if (msg == null)
            {
                return;
            }

            // 调用 activePanel 的 Translate 方法
            foreach (var panel in _activePanels.Values)
            {
                panel.Translate(msg.Language);
            }
        }

        public Vector2 WorldToScreen(Vector3 worldPosition)
        {
            var screenPosition = _UiCamera.WorldToScreenPoint(worldPosition);
            return screenPosition;
        }

        public Vector3 ScreenToWorld(Vector2 uiPosition)
        {
            var worldPosition = _UiCamera.ScreenToWorldPoint(uiPosition);
            return worldPosition;
        }

        public bool RectangleContainsScreenPoint(RectTransform rect, Vector2 screenPoint, Vector2 offset)
        {
            return RectTransformUtility.RectangleContainsScreenPoint(rect, screenPoint, _UiCamera, offset);
        }
    }
}