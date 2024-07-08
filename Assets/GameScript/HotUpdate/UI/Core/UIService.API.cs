using Cysharp.Threading.Tasks;
using Game.Log;
using UnityEngine;

namespace Game
{
    public partial class UIService
    {
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

        public UI2DPanel OpenPanel(string className)
        {
            if (!TryGetPanelInfo(className, out var panelInfo))
            {
                GameLog.Error($"没有找到UI2DPanelAttribute特性的类型{className}");
                return null;
            }
            if (!_activePanels.TryGetValue(className, out var panel))
            {
                panel = GetPanel(className, panelInfo);
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
            panel.Open();
            return panel;
        }

        private void ClosePanel(UI2DPanel panel)
        {
            if (panel == null)
            {
                return;
            }
            _activePanels.Remove(panel.GetType().Name);
            RecyclePanel(panel);
        }
    }
}
