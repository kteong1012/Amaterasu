using Game.Log;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public partial class UIService
    {
        private Dictionary<string, YooAssetGameObjectPool> _panelPools = new Dictionary<string, YooAssetGameObjectPool>();

        private const float _disposePanelPoolInterval = 30f;
        private float _disposePanelPoolTimer = 0f;

        private void UpdatePanelPool()
        {
            _disposePanelPoolTimer += Time.deltaTime;
            if (_disposePanelPoolTimer >= _disposePanelPoolInterval)
            {
                _disposePanelPoolTimer -= _disposePanelPoolInterval;

                var removeKeys = new HashSet<string>();
                foreach (var pair in _panelPools)
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
                    _panelPools.Remove(removeKey);
                }
            }
        }

        private UI2DPanel GetPanel(UI2DPanelInfo info)
        {
            if (!_panelPools.TryGetValue(info.prefabPath, out var pool))
            {
                pool = new YooAssetGameObjectPool(info.prefabPath, OnCreateUIPanel, null, null, OnDestroyUIPanel, true, 1, 1);
                _panelPools.Add(info.prefabPath, pool);
            }
            return pool.GetAsComponent<UI2DPanel>(null);
        }

        private void OnCreateUIPanel(GameObject go)
        {
            var panel = go.GetComponent<UI2DPanel>();
            panel.__Init();
        }

        private void OnDestroyUIPanel(GameObject go)
        {
            var panel = go.GetComponent<UI2DPanel>();
            panel.__Release();
        }

        private void OnGetUIPanel(GameObject go)
        {
            var panel = go.GetComponent<UI2DPanel>();
            panel.__Show();
        }

        private void OnReleaseUIPanel(GameObject go)
        {
            var panel = go.GetComponent<UI2DPanel>();
            panel.__Hide();
        }

        private void RecyclePanel(UI2DPanel panel)
        {
            var typeName = panel.ClassName;
            TryGetPanelInfo(typeName, out var panelInfo);
            if (!_panelPools.TryGetValue(panelInfo.prefabPath, out var pool))
            {
                GameLog.Error($"回收界面异常，没有找到PanelPool '{panelInfo.prefabPath}'");
                return;
            }
            pool.Release(panel.gameObject);
        }
    }
}
