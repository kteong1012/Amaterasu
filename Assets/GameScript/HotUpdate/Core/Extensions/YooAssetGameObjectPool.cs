using System;
using UnityEngine;
using UnityEngine.Pool;
using YooAsset;

namespace Game
{
    public class YooAssetGameObjectPool : IDisposable
    {
        private readonly string _assetPath;
        private AssetHandle _handle;
        private ObjectPool<GameObject> _pool;

        public YooAssetGameObjectPool(string assetPath, Action<GameObject> actionOnGet = null, Action<GameObject> actionOnRelease = null, Action<GameObject> actionOnDestroy = null, bool collectionCheck = true, int defaultCapacity = 10, int maxSize = 10000)
        {
            _pool = new ObjectPool<GameObject>(CreateFunc, actionOnGet, actionOnRelease, actionOnDestroy, collectionCheck, defaultCapacity, maxSize);
            _assetPath = assetPath;
        }

        public GameObject Get(Transform parent)
        {
            var go = _pool.Get();
            if (go == null)
            {
                return null;
            }

            go.transform.SetParent(parent);
            go.transform.localPosition = Vector3.zero;
            go.transform.localRotation = Quaternion.identity;
            go.transform.localScale = Vector3.one;
            go.SetActive(true);
            return go;
        }

        public T Get<T>(Transform parent) where T : Component
        {
            var go = Get(parent);
            if (go == null)
            {
                return null;
            }
            return go.GetComponent<T>();
        }

        public void Release(GameObject obj)
        {
            _pool.Release(obj);
            obj.transform.SetParent(null);
            obj.SetActive(false);
        }

        #region ObjectPool
        private GameObject CreateFunc()
        {
            if (_handle == null)
            {
                _handle = YooAssets.LoadAssetSync<GameObject>(_assetPath);
            }

            var go = _handle.InstantiateSync();
            go.name = $"{_assetPath}_{go.name}";
            return go;
        }

        public void Dispose()
        {
            _pool.Dispose();
            _handle?.Release();
        }
        #endregion
    }
}