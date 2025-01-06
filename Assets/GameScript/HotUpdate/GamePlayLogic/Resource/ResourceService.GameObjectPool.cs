using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using System.Text;
using UnityEngine;
using YooAsset;

namespace Game
{
    public struct GameObjectPoolWrapper
    {
        public string AssetPath;
        public GameObject GameObject;
    }

    public partial class ResourceService
    {
        private static readonly int _poolFrameLimit = 500;

        private readonly Dictionary<string, GameObjectPool> _gameObjectPools = new();

        public GameObjectPoolWrapper GetGameObjectFromPool(string assetPath, Transform parent, Action<GameObject> onCreate = null, Action<GameObject> actionOnGet = null, Action<GameObject> actionOnRelease = null, Action<GameObject> actionOnDestroy = null, bool collectionCheck = true, int defaultCapacity = 10, int maxSize = 10000)
        {
            if (!_gameObjectPools.TryGetValue(assetPath, out var pool))
            {
                pool = new GameObjectPool(assetPath, onCreate, actionOnGet, actionOnRelease, actionOnDestroy, collectionCheck, defaultCapacity, maxSize);
                _gameObjectPools.Add(assetPath, pool);
            }

            var go = pool.Get(parent);
            return new GameObjectPoolWrapper
            {
                AssetPath = assetPath,
                GameObject = go
            };
        }

        public void RecycleGameObject(string assetPath, GameObject go)
        {
            if (!_gameObjectPools.TryGetValue(assetPath, out var pool))
            {
                Debug.LogError($"GameObjectPool not found for assetPath: {assetPath}");
                return;
            }

            pool.Release(go);
        }

        public void RecycleGameObject(GameObjectPoolWrapper wrapper)
        {
            RecycleGameObject(wrapper.AssetPath, wrapper.GameObject);
        }

        private void UpdateGameObjectPools()
        {
            var keysToRemove = new List<string>();
            foreach (var (path, pool) in _gameObjectPools)
            {
                pool.Update();
                if (pool.IsIdleFrameGraterThan(_poolFrameLimit))
                {
                    keysToRemove.Add(path);
                }
            }

            foreach (var key in keysToRemove)
            {
                _gameObjectPools.Remove(key);
            }
        }
    }
}