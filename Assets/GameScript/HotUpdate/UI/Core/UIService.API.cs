using Cysharp.Threading.Tasks;
using Game.Log;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public partial class UIService
    {
        private Dictionary<string, UI2DPanel> _activePanels = new Dictionary<string, UI2DPanel>();

        public static T OpenPanel<T>() where T : UI2DPanel
        {
            var panel = Instance.__OpenPanel<T>();
            if (panel == null)
            {
                GameLog.Error($"没有找到UI2DPanel '{typeof(T).Name}'");
                return null;
            }

            return panel;
        }

        public static T CreateNode<T>(Transform parent) where T : UI2DNode
        {
            var node = Instance.__CreateNode<T>();
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

        public UI2DPanel OpenPanel(string className)
        {
            return __OpenPanel(className);
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

        private void ClosePanel(UI2DPanel panel)
        {
            if (panel == null)
            {
                return;
            }
            panel.__Hide();
            _activePanels.Remove(panel.GetType().Name);
            RecyclePanel(panel);
        }
    }
}
