using Cysharp.Threading.Tasks;
using Game.Log;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public partial class UIService
    {
        private Dictionary<string, UI2DPanel> _activePanels = new Dictionary<string, UI2DPanel>();

        public T OpenPanel<T>() where T : UI2DPanel
        {
            var className = __GetPanelClassName<T>();
            var panel = __OpenPanel(className) as T;
            if (panel == null)
            {
                GameLog.Error($"没有找到UI2DPanel '{typeof(T).Name}'");
                return null;
            }

            return panel;
        }

        public T CreateNode<T>(Transform parent) where T : UI2DNode
        {
            var className = __GetNodeClassName<T>();
            var node = __GetNode(className) as T;
            if (node == null)
            {
                GameLog.Error($"没有找到UI2DNode '{typeof(T).Name}'");
                return null;
            }

            node.transform.SetParent(parent, false);
            node.transform.SetAsLastSibling();
            node.transform.localPosition = Vector3.zero;
            node.transform.localScale = Vector3.one;
            node.transform.localRotation = Quaternion.identity;

            return node;
        }
        public bool TryGetPanel<T>(out T panel) where T : UI2DPanel
        {
            var className = __GetPanelClassName<T>();
            if (_activePanels.TryGetValue(className, out var p))
            {
                panel = p as T;
                return true;
            }
            panel = null;
            return false;
        }

        public void ClosePanel<T>() where T : UI2DPanel
        {
            var className = __GetPanelClassName<T>();
            if (_activePanels.TryGetValue(className, out var panel))
            {
                ClosePanel(panel);
            }
        }

        private UI2DPanel __OpenPanel(string className)
        {
            if (!TryGetPanelInfo(className, out var panelInfo))
            {
                GameLog.Error($"没有找到UI2DPanelAttribute特性的类型{className}");
                return null;
            }
            if (!_activePanels.TryGetValue(className, out var panel))
            {
                panel = GetPanel(panelInfo);
                if (panel == null)
                {
                    GameLog.Error($"获取 Panel 失败 '{className}'");
                    return null;
                }
                _activePanels[className] = panel;
                SetParent(panel.transform, panel.Layer);
                panel.gameObject.SetActive(true);
                panel.transform.SetAsLastSibling();
                panel.transform.localPosition = Vector3.zero;
                panel.transform.localScale = Vector3.one;
                panel.transform.localRotation = Quaternion.identity;
                panel.__Show();
                return panel;
            }
            else
            {
                panel.transform.SetAsLastSibling();
                panel.__Show();
                return panel;
            }
        }

        private void ClosePanel(UI2DPanel panel)
        {
            if (panel == null || !panel.IsShow)
            {
                return;
            }
            panel.__Hide();
            _activePanels.Remove(panel.ClassName);
            RecyclePanel(panel);
        }
        public bool TryGetPanelInfo(string panelName, out UI2DInfo panelInfo)
        {
            return UI2DPanelReflectionHelper.TryGetPanelInfo(panelName, out panelInfo);
        }

        private string __GetPanelClassName<T>() where T : UI2DPanel
        {
            return UI2DPanelReflectionHelper.GetPanelClassName<T>();
        }
        public bool TryGetNodeInfo(string nodeName, out UI2DInfo nodeInfo)
        {
            return UI2DNodeReflectionHelper.TryGetNodeInfo(nodeName, out nodeInfo);
        }

        private string __GetNodeClassName<T>() where T : UI2DNode
        {
            return UI2DNodeReflectionHelper.GetNodeClassName<T>();
        }
    }
}
