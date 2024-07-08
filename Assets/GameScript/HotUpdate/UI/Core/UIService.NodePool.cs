using Game.Log;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public partial class UIService
    {
        private Dictionary<string, YooAssetGameObjectPool> _nodePools = new Dictionary<string, YooAssetGameObjectPool>();

        private const float _disposeNodePoolInterval = 30f;
        private float _disposeNodePoolTimer = 0f;

        private void UpdateNodePool()
        {
            _disposeNodePoolTimer += Time.deltaTime;
            if (_disposeNodePoolTimer >= _disposeNodePoolInterval)
            {
                _disposeNodePoolTimer -= _disposeNodePoolInterval;

                var removeKeys = new HashSet<string>();
                foreach (var pair in _nodePools)
                {
                    var poolName = pair.Key;
                    var pool = pair.Value;
                    if (pool.RefCount == 0)
                    {
                        GameLog.Debug($"自动清除对象池： '{poolName}'");
                        pool.Dispose();
                    }
                }
                foreach (var removeKey in removeKeys)
                {
                    _nodePools.Remove(removeKey);
                }
            }
        }

        public UI2DNode GetNode(string className)
        {
            return __GetNode(className);
        }

        private UI2DNode __GetNode(string className)
        {
            if (!TryGetNodeInfo(className, out var info))
            {
                GameLog.Error($"没有找到UI2DNodeAttribute特性的类型{className}");
                return null;
            }
            if (!_nodePools.TryGetValue(info.prefabPath, out var pool))
            {
                pool = new YooAssetGameObjectPool(info.prefabPath, OnCreateUINode, OnGetUINode, OnReleaseUINode, OnDestroyUINode);
                _nodePools.Add(info.prefabPath, pool);
            }
            return pool.GetAsComponent<UI2DNode>(null);
        }

        private void OnCreateUINode(GameObject go)
        {
            var node = go.GetComponent<UI2DNode>();
            node.__Init();
        }

        private void OnGetUINode(GameObject go)
        {
            var node = go.GetComponent<UI2DNode>();
            node.__Show();
        }

        private void OnReleaseUINode(GameObject go)
        {
            var node = go.GetComponent<UI2DNode>();
            node.__Hide();
        }

        private void OnDestroyUINode(GameObject go)
        {
            var node = go.GetComponent<UI2DNode>();
            node.__Release();
        }

        private void RecycleNode(UI2DNode node)
        {
            var typeName = node.ClassName;
            TryGetNodeInfo(typeName, out var nodeInfo);
            if (!_nodePools.TryGetValue(nodeInfo.prefabPath, out var pool))
            {
                GameLog.Error($"回收界面异常，没有找到NodePool '{nodeInfo.prefabPath}'");
                return;
            }
            node.gameObject.SetActive(false);
            pool.Release(node.gameObject);
        }
    }
}
