using Cysharp.Threading.Tasks;
using Game.Log;
using System.Collections.Generic;
using UnityEngine;
using YooAsset;

namespace Game
{
    public enum UI2DPanelLayer
    {
        Background = 0,
        Normal,
        Popup,
        Top,
        Tips,
        Loading,
    }
    public partial class UIService
    {

        private Dictionary<UI2DPanelLayer,Transform> _layerRoots = new Dictionary<UI2DPanelLayer, Transform>();

        private void InitLayer()
        {
            // 初始化层级
            foreach (var layer in (UI2DPanelLayer[])System.Enum.GetValues(typeof(UI2DPanelLayer)))
            {
                var layerRoot = new GameObject(layer.ToString());
                layerRoot.transform.SetParent(_layerRoot);
                layerRoot.transform.localPosition = Vector3.zero;
                layerRoot.transform.localScale = Vector3.one;
                layerRoot.transform.localRotation = Quaternion.identity;
                layerRoot.transform.SetAsLastSibling();
                _layerRoots.Add(layer, layerRoot.transform);
            }
        }
        private void SetParent(Transform transform, UI2DPanelLayer layer)
        {
            transform.SetParent(_layerRoots[layer]);
        }
    }
}
