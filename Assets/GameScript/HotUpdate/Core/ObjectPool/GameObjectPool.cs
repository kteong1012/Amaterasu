using Game.Log;
using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Pool;
using YooAsset;

namespace Game
{
    public class GameObjectPool : IDisposable
    {
        private readonly string _assetPath;
        private AssetHandle _handle;
        private GameObject _prefab;
        private ObjectPool<GameObject> _pool;

        private Action<GameObject> _onCreate;
        private Action<GameObject> _actionOnGet;
        private Action<GameObject> _actionOnRelease;
        private Action<GameObject> _actionOnDestroy;

        private int _refCount;
        public int RefCount => _refCount;

        private int _zeroRefCountFrameCount;

        public GameObjectPool(string assetPath, Action<GameObject> onCreate = null, Action<GameObject> actionOnGet = null, Action<GameObject> actionOnRelease = null, Action<GameObject> actionOnDestroy = null, bool collectionCheck = true, int defaultCapacity = 10, int maxSize = 10000)
        {
            _onCreate = onCreate;
            _assetPath = assetPath;
            _actionOnGet = actionOnGet;
            _actionOnRelease = actionOnRelease;
            _actionOnDestroy = actionOnDestroy;

            _pool = new ObjectPool<GameObject>(CreateByPath, ActionOnGet, ActionOnRelease, ActionOnDestroy, collectionCheck, defaultCapacity, maxSize);

            _refCount = 0;
        }

        public GameObjectPool(GameObject prefab, Action<GameObject> onCreate = null, Action<GameObject> actionOnGet = null, Action<GameObject> actionOnRelease = null, Action<GameObject> actionOnDestroy = null, bool collectionCheck = true, int defaultCapacity = 10, int maxSize = 10000)
        {
            _onCreate = onCreate;
            _prefab = prefab;
            _actionOnGet = actionOnGet;
            _actionOnRelease = actionOnRelease;
            _actionOnDestroy = actionOnDestroy;

            _pool = new ObjectPool<GameObject>(CreateByPrefab, ActionOnGet, ActionOnRelease, ActionOnDestroy, collectionCheck, defaultCapacity, maxSize);

            _refCount = 0;
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

            _refCount++;
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
            obj.transform.SetParent(null);
            obj.SetActive(false);
            _pool.Release(obj);
            _refCount--;
        }

        public void Dispose()
        {
            _pool.Dispose();
            _handle?.Release();
            _handle = null;
            _prefab = null;
            _actionOnGet = null;
            _actionOnRelease = null;
            _actionOnDestroy = null;
            _refCount = 0;
        }

        public void Update()
        {
            if (_refCount == 0)
            {
                _zeroRefCountFrameCount++;
            }
            else
            {
                _zeroRefCountFrameCount = 0;
            }
        }

        public bool IsIdleFrameGraterThan(int frameCount)
        {
            return _zeroRefCountFrameCount >= frameCount;
        }

        #region ObjectPool

        private GameObject CreateByPath()
        {
            if (_handle == null)
            {
                _handle = SSS.Get<ResourceService>().LoadAssetSync<GameObject>(_assetPath);
            }

            if (_handle == null || _handle.Status == EOperationStatus.Failed)
            {
                GameLog.Error($"GameObjectPool CreateByPath Failed: {_assetPath}");
                return null;
            }

            var go = _handle.InstantiateSync();
            go.name = _assetPath;
            _onCreate?.Invoke(go);
            return go;
        }

        private GameObject CreateByPrefab()
        {
            var go = UnityEngine.Object.Instantiate(_prefab);
            go.name = $"{_prefab.name}";
            _onCreate?.Invoke(go);
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