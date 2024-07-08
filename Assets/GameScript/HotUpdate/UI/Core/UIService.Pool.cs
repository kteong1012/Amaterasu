using Game.Log;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public partial class UIService
    {
        private Dictionary<string, UI2DPanel> _recycledSingletonPanels = new Dictionary<string, UI2DPanel>();

        private Dictionary<string, List<UI2DPanel>> _recycledMultiPanels = new Dictionary<string, List<UI2DPanel>>();

        private Dictionary<string, YooAssetGameObjectPool> _uiPools = new Dictionary<string, YooAssetGameObjectPool>();

        private const float _disposeInterval = 60f;
        private float _disposeTimer = 0f;

        private void UpdatePool()
        {
            _disposeTimer += Time.deltaTime;
            if (_disposeTimer >= _disposeInterval)
            {
                _disposeTimer -= _disposeInterval;

                // 遍历回收池，销毁_activePanels中没有的panel
                var keys = new List<string>(_recycledSingletonPanels.Keys);
                foreach (var key in keys)
                {
                    if (!_activePanels.ContainsKey(key))
                    {
                        var panel = _recycledSingletonPanels[key];
                        _recycledSingletonPanels.Remove(key);
                        panel.OnRelease();
                        UnityEngine.Object.Destroy(panel.gameObject);
                    }
                }
            }
        }

        private UI2DPanel GetPanel(string typeName, UI2DPanelInfo info)
        {
            UI2DPanel panel = null;
            if (info.allowMultiOpen)
            {
                panel = GetMultiPanel(typeName, info);
            }
            else
            {
                panel = GetSingletonPanel(typeName, info);
            }
            return panel;
        }

        private UI2DPanel GetSingletonPanel(string typeName, UI2DPanelInfo info)
        {
            if (_recycledSingletonPanels.TryGetValue(typeName, out var panel))
            {
                _recycledSingletonPanels.Remove(typeName);
                return panel;
            }
            else
            {
                return CreatePanel(typeName, info);
            }
        }

        private UI2DPanel GetMultiPanel(string typeName, UI2DPanelInfo info)
        {
            if (_recycledMultiPanels.TryGetValue(typeName, out var panels) && panels.Count > 0)
            {
                var panel = panels[0];
                panels.RemoveAt(0);
                return panel;
            }
            else
            {
                return CreatePanel(typeName, info);
            }
        }

        private UI2DPanel CreatePanel(string typeName, UI2DPanelInfo info)
        {
            if (!_uiPools.TryGetValue(info.prefabPath, out var pool))
            {
                pool = new YooAssetGameObjectPool(info.prefabPath);
                _uiPools.Add(info.prefabPath, pool);
            }

            var panel = pool.GetAsComponent<UI2DPanel>(null);
            if (panel == null)
            {
                GameLog.Error($"UI2DPanelInfo.prefabPath指定的预制体{info.prefabPath}上没有UI2DPanel组件");
                return null;
            }
            panel.gameObject.SetActive(false);
            panel.OnCreate();
            return panel;
        }

        private void RecyclePanel(UI2DPanel panel)
        {
            var typeName = panel.ClassName;
            TryGetPanelInfo(typeName, out var panelInfo);
            if (panelInfo.allowMultiOpen)
            {
                if (!_recycledMultiPanels.TryGetValue(typeName, out var panels))
                {
                    panels = new List<UI2DPanel>();
                    _recycledMultiPanels.Add(typeName, panels);
                }
                panels.Add(panel);
            }
            else
            {
                _recycledSingletonPanels.Add(typeName, panel);
            }
            if (_uiPools.TryGetValue(panelInfo.prefabPath, out var pool))
            {
                panel.gameObject.SetActive(false);
                pool.Release(panel.gameObject);
            }
        }
    }
}
