using UnityEngine;
using YooAsset;

namespace Game
{
    public class UnitModelComponent : UnitComponent
    {
        private AssetHandle _handle;
        private GameObject _model;
        private Animation _animation;

        protected override void OnInit()
        {
            var unitData = _controller.UnitData;
            var handle = YooAssets.LoadAssetSync<GameObject>(unitData.ModelPath);
            _model = handle.InstantiateSync(Vector3.zero, Quaternion.identity, _controller.transform);
            _animation = _model.GetOrAddComponent<Animation>();
        }

        protected override void OnRelease()
        {
            if (_handle != null)
            {
                _handle.Release();
            }
        }
    }
}
