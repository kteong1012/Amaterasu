using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;
using YooAsset;

namespace Game
{
    public abstract class UI2D : MonoBehaviour
    {
        private List<UI2DNode> _children = new List<UI2DNode>();

        private List<AssetHandle> _assetHandles = new List<AssetHandle>();

        private void ClearAssetHandles()
        {
            foreach (var handle in _assetHandles)
            {
                handle.Release();
            }
            _assetHandles.Clear();
        }

        public T AddChildNode<T>(Transform parent = null) where T : UI2DNode
        {
            if (parent == null)
            {
                parent = transform;
            }
            var node = SSS.Get<UIService>().CreateNode<T>(parent);
            _children.TryAdd(node);
            return node;
        }

        public void AddChildNode<T>(T node) where T : UI2DNode
        {
            // 手动唤起生命周期
            node.__Init();
            node.__Show();
            _children.TryAdd(node);
        }

        private bool _inited;

        protected bool _isShow;
        public virtual bool IsShow { get => _isShow; protected set => _isShow = value; }

        public void __Show(bool force = false)
        {
            if (!_inited)
            {
                __Init();
            }
            else if (IsShow && !force)
            {
                return;
            }
            IsShow = true;
            foreach (var child in _children)
            {
                child.__Show(force);
            }
            gameObject.SetActive(true);
            OnShow();
        }

        public void __Hide()
        {
            if (!_inited)
            {
                __Init();
            }
            else if (!IsShow)
            {
                return;
            }
            IsShow = false;
            foreach (var child in _children)
            {
                child.__Hide();
            }
            OnHide();
            gameObject.SetActive(false);
        }



        public void RemoveChild(UI2DNode node)
        {
            node.__Release();
            _children.TryRemove(node);
        }

        protected void ReleaseChildren()
        {
            foreach (var child in _children)
            {
                child.__Release();
            }
            _children.Clear();

        }

        public void __Init()
        {
            if (_inited)
            {
                return;
            }
            _inited = true;
            OnCreate();
        }

        public void __Release()
        {
            ReleaseChildren();
            __Hide();
            OnRelease();
        }

        /// <summary>
        /// 创建时调用
        /// </summary>
        protected virtual void OnCreate()
        {

        }

        /// <summary>
        /// 销毁时调用
        /// </summary>
        protected virtual void OnRelease()
        {
        }

        /// <summary>
        /// 每次打开面板时调用
        /// </summary>
        protected virtual void OnShow()
        {

        }

        /// <summary>
        /// 每次关闭面板时调用
        /// </summary>
        protected virtual void OnHide()
        {

        }

        /// <summary>
        /// 增加此接口是为了切换多语言时候能调用
        /// </summary>
        public void Translate(Language language)
        {
            // 遍历所有的 TextMeshProUGUI 组件，把它们的字体批量替换
            var textComponents = transform.GetComponentsInChildren<TMPro.TextMeshProUGUI>(true);
            var font = Localization.LocalizationHandler.GetFont();
            foreach (var textComponent in textComponents)
            {
                textComponent.font = font;
            }
            foreach (var child in _children)
            {
                child.Translate(language);
            }
            UpdateLocalizationText(language);
            UpdateView();
        }

        /// <summary>
        /// 遍历物体下所有 Localization 组件，调用 Refresh 方法
        /// </summary>
        /// <param name="language"></param>
        private void UpdateLocalizationText(Language language)
        {
            var localizations = GetComponentsInChildren<Localization>(true);
            foreach (var localization in localizations)
            {
                localization.Refresh();
            }
        }

        protected virtual void UpdateView()
        {

        }
    }
}

