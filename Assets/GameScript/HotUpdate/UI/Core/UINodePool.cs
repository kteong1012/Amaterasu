using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class UINodePool<TData>
    {
        private readonly GameObject _prefab;
        private readonly Transform _parent;

        private readonly List<GameObject> _pool = new List<GameObject>();
        private readonly List<GameObject> _using = new List<GameObject>();

        public Action<GameObject, TData> onGetItem;

        public UINodePool(GameObject prefab, Transform parent)
        {
            _prefab = prefab;
            _parent = parent;
        }

        public void SetData(IEnumerable<TData> datas, Action<GameObject, TData> onRenderItem)
        {
            onGetItem = onRenderItem;
            Clear();
            foreach (var data in datas)
            {
                var item = GetItem();
                onGetItem?.Invoke(item, data);
            }
        }

        public void Clear()
        {
            foreach (var item in _using)
            {
                item.SetActive(false);
                _pool.Add(item);
            }
            _using.Clear();
        }

        public GameObject GetItem()
        {
            GameObject item = null;
            if (_pool.Count > 0)
            {
                item = _pool[0];
                _pool.RemoveAt(0);
            }
            else
            {
                item = GameObject.Instantiate(_prefab, _parent);
            }
            item.gameObject.SetActive(true);
            _using.Add(item);
            return item;
        }

        public void RecycleItem(GameObject item)
        {
            item.SetActive(false);
            _pool.Add(item);
            _using.Remove(item);
        }

        public void RecycleAll()
        {
            foreach (var item in _using)
            {
                item.SetActive(false);
                _pool.Add(item);
            }
            _using.Clear();
        }
    }
    public class UINodePool<TNode, TData> where TNode : UI2DNode
    {
        private Transform _parent;

        private List<TNode> _pool = new List<TNode>();
        private List<TNode> _using = new List<TNode>();

        public UINodePool(Transform parent)
        {
            _parent = parent;
        }

        public void SetData(IEnumerable<TData> datas, Action<TNode, TData> onRenderItem)
        {
            Clear();
            foreach (var data in datas)
            {
                var item = GetItem();
                onRenderItem?.Invoke(item, data);
            }
        }

        public void Clear()
        {
            foreach (var item in _using)
            {
                item.gameObject.SetActive(false);
                _pool.Add(item);
            }
            _using.Clear();
        }

        public TNode GetItem()
        {
            TNode item = null;
            if (_pool.Count > 0)
            {
                item = _pool[0];
                _pool.RemoveAt(0);
            }
            else
            {
                item = SSS.Get<UIService>().CreateNode<TNode>(_parent);
            }
            item.gameObject.SetActive(true);
            _using.Add(item);
            return item;
        }

        public void RecycleItem(TNode item)
        {
            item.gameObject.SetActive(false);
            _pool.Add(item);
            _using.Remove(item);
        }

        public void RecycleAll()
        {
            foreach (var item in _using)
            {
                item.gameObject.SetActive(false);
                _pool.Add(item);
            }
            _using.Clear();
        }
    }
}
