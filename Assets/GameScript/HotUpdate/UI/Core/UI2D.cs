using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public abstract class UI2D : MonoBehaviour
    {
        private List<UI2DNode> _children = new List<UI2DNode>();

        public UI2DNode CreateChild<T>(Transform parent = null) where T : UI2DNode
        {
            if (parent == null)
            {
                parent = transform;
            }
            var node = UIService.CreateNode<T>(parent);
            _children.TryAdd(node);
            return node;
        }

        private bool _isShow = false;

        public void __Show()
        {
            if (_isShow)
            {
                return;
            }
            _isShow = true;
            foreach (var child in _children)
            {
                child.__Show();
            }
            gameObject.SetActive(true);
            OnShow();
        }

        public void __Hide()
        {
            if (!_isShow)
            {
                return;
            }
            _isShow = false;
            foreach (var child in _children)
            {
                child.__Hide();
            }
            OnHide();
            gameObject.SetActive(false);
        }



        public void ReleaseChild(UI2DNode node)
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
    }
}

