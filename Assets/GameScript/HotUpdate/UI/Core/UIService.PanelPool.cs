using Game.Log;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public partial class UIService
    {
        private Dictionary<string, GameObjectPool> _panelPools = new Dictionary<string, GameObjectPool>();
        private HashSet<string> _dontNeedToClearPoolNames = new HashSet<string>();

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
                    if (_dontNeedToClearPoolNames.Contains(poolName))
                    {
                        continue;
                    }
                    if (pool.RefCount == 0)
                    {
                        GameLog.Debug($"自动清除对象池： '{poolName}'");
                        pool.Dispose();
                        removeKeys.Add(poolName);
                    }
                }
                foreach (var removeKey in removeKeys)
                {
                    _panelPools.Remove(removeKey);
                }
            }
        }

        private UI2DPanel GetPanel(UI2DInfo info)
        {
            if (!_panelPools.TryGetValue(info.prefabPath, out var pool))
            {
                pool = new GameObjectPool(info.prefabPath, OnCreateUIPanel, OnGetUIPanel, OnReleaseUIPanel, OnDestroyUIPanel, true, 1, 1);
                _panelPools.Add(info.prefabPath, pool);
            }
            return pool.GetAsComponent<UI2DPanel>(null);
        }

        private void OnCreateUIPanel(GameObject go)
        {
            var panel = go.GetComponent<UI2DPanel>();
            panel.__Init();

            // 定制代码，检测子物体有没有 CloseButton，有的话绑定关闭事件
            var closeButtons = panel.GetComponentsInChildren<ButtonClose>();
            foreach (var closeButton in closeButtons)
            {
                closeButton.onClick.AddListener(() =>
                {
                    UIEventDefine.UI2DPanelCloseEvent.SendMsg(panel);
                });
            }
            if (!panel.IsAutoRelease)
            {
                _dontNeedToClearPoolNames.Add(panel.ClassName);
            }
        }

        private void OnDestroyUIPanel(GameObject go)
        {
            var panel = go.GetComponent<UI2DPanel>();
            panel.__Release();
        }

        private void OnGetUIPanel(GameObject go)
        {
            var panel = go.GetComponent<UI2DPanel>();
            panel.Translate(SSS.Get<Game.Cfg.ConfigService>().Language);
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
