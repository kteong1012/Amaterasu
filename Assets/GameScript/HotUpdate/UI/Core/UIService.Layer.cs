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
        Bottom,
        Main,
        Effect,
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
                layerRoot.transform.SetParent(_LayerRoot);
                layerRoot.transform.localScale = Vector3.one;
                layerRoot.transform.localRotation = Quaternion.identity;
                layerRoot.transform.SetAsLastSibling();
                
                // 和Main层级比较，比Main小的，Z轴-100，比Main大的，Z轴+100
                if (layer < UI2DPanelLayer.Main)
                {
                    layerRoot.transform.localPosition = new Vector3(0, 0, 100);
                }
                else if (layer > UI2DPanelLayer.Main)
                {
                    layerRoot.transform.localPosition = new Vector3(0, 0, -100);
                }
                else
                {
                    layerRoot.transform.localPosition = Vector3.zero;
                }
                
                _layerRoots.Add(layer, layerRoot.transform);
            }
        }
        private void SetParent(Transform transform, UI2DPanelLayer layer)
        {
            transform.SetParent(_layerRoots[layer]);
        }
        
        public Transform GetLayerRoot(UI2DPanelLayer layer)
        {
            return _layerRoots[layer];
        }
    }
}
