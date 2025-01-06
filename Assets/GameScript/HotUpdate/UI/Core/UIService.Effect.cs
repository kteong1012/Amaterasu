using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Game.Log;
using UnityEngine;

namespace Game
{
    public partial class UIService
    {
        private Transform _effectParent => GetLayerRoot(UI2DPanelLayer.Effect);

        private readonly Dictionary<int, string> _effectNameMap = new();

        public GameObject GetUIEffect(string effectName)
        {
            var wrapper = SSS.Get<ResourceService>().GetGameObjectFromPool(effectName, _effectParent);
            if (!wrapper.GameObject)
            {
                GameLog.Error($"没有找到特效 '{effectName}'");
                return null;
            }

            _effectNameMap.Add(wrapper.GameObject.GetInstanceID(), effectName);

            return wrapper.GameObject;
        }

        public void ReleaseUIEffect(GameObject go)
        {
            var instanceId = go.GetInstanceID();
            if (_effectNameMap.TryGetValue(instanceId, out var effectName))
            {
                SSS.Get<ResourceService>().RecycleGameObject(effectName, go);
                _effectNameMap.Remove(instanceId);
            }
        }
    }
}