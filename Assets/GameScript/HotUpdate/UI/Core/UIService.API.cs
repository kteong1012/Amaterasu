using Cysharp.Threading.Tasks;
using Game.Log;
using UnityEngine;

namespace Game
{
    public partial class UIService
    {
        public static T OpenPanel<T>() where T : UI2DPanel
        {
            return Instance.DoOpenPanel<T>();
        }

        private T DoOpenPanel<T>() where T : UI2DPanel
        {
            var type = typeof(T);
            if (!ui2DPanelInfos.TryGetValue(type.Name, out var panelInfo))
            {
                GameLog.Error($"没有找到UI2DPanelAttribute特性的类型{type}");
                return null;
            }
            if (!_activePanels.TryGetValue(type.Name, out var panel))
            {
                panel = GetPanel(type.Name, panelInfo);
                if (panel == null)
                {
                    GameLog.Error($"获取面板失败{type}");
                    return null;
                }
            }
            _activePanels[type.Name] = panel;
            SetParent(panel.transform, panel.Layer);
            panel.gameObject.SetActive(true);
            panel.transform.SetAsLastSibling();
            panel.transform.localPosition = Vector3.zero;
            panel.transform.localScale = Vector3.one;
            panel.transform.localRotation = Quaternion.identity;
            panel.Open();
            return panel as T;
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
