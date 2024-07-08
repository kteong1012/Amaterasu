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

        private Action<GameObject> _actionOnGet;
        private Action<GameObject> _actionOnRelease;
        private Action<GameObject> _actionOnDestroy;

        private bool _disposed;

        public YooAssetGameObjectPool(string assetPath, Action<GameObject> actionOnGet = null, Action<GameObject> actionOnRelease = null, Action<GameObject> actionOnDestroy = null, bool collectionCheck = true, int defaultCapacity = 10, int maxSize = 10000)
        {
            _disposed = false;

            _assetPath = assetPath;
            _actionOnGet = actionOnGet;
            _actionOnRelease = actionOnRelease;
            _actionOnDestroy = actionOnDestroy;

            _pool = new ObjectPool<GameObject>(CreateFunc, ActionOnGet, ActionOnRelease, ActionOnDestroy, collectionCheck, defaultCapacity, maxSize);
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

        public T GetAsComponent<T>(Transform parent) where T : Component
        {
            var go = Get(parent);
            if (go == null)
            {
                return null;
            }
            return go.GetComponent<T>();
        }

        /// <summary>
        /// 回收物体
        /// </summary>
        /// <param name="obj">要回收的物体</param>
        /// <param name="onDestroyAction">如果对象池在调用该方法的时候已经被Dispose了，就会调用这个委托。</param>
        public void Release(GameObject obj, Action<GameObject> onDestroyAction = null)
        {
            if (_disposed)
            {
                onDestroyAction?.Invoke(obj);
                UnityEngine.Object.Destroy(obj);
            }
            else
            {
                obj.transform.SetParent(null);
                obj.SetActive(false);
                _pool.Release(obj);
            }
        }

        public void Dispose()
        {
            _disposed = true;
            _pool.Dispose();
            _handle?.Release();
            _actionOnGet = null;
            _actionOnRelease = null;
            _actionOnDestroy = null;
        }

        #region ObjectPool
        private GameObject CreateFunc()
        {
            if (_handle == null)
            {
                _handle = GameServices.ResourceService.LoadAssetSync<GameObject>(_assetPath);
            }

            var go = _handle.InstantiateSync();
            go.name = $"{_assetPath}_{go.name}";
            return go;
        }

        private void ActionOnGet(GameObject obj)
        {
            _actionOnGet?.Invoke(obj);
        }

        private void ActionOnRelease(GameObject obj)
        {
            _actionOnRelease?.Invoke(obj);
        }

        private void ActionOnDestroy(GameObject obj)
        {
            _actionOnDestroy?.Invoke(obj);
            UnityEngine.Object.Destroy(obj);
        }
        #endregion
    }
}