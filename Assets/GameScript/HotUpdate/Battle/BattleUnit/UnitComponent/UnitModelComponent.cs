using System;
using UniFramework.Event;
using UnityEngine;
using YooAsset;

namespace Game
{
    public class UnitModelComponent : UnitComponent
    {
        private AssetHandle _handle;
        private GameObject _model;
        private Animation _animation;
        private EventGroup _eventGroup = new EventGroup();

        protected override void OnInit()
        {
            var unitData = _controller.UnitData;
            var handle = YooAssets.LoadAssetSync<GameObject>(unitData.ModelPath);
            _model = handle.InstantiateSync(Vector3.zero, Quaternion.identity, _controller.transform);
            _animation = _model.GetOrAddComponent<Animation>();

            _eventGroup.AddListener<UnitAttributeChangeEvent>(OnAttributeChange);
        }

        protected override void OnRelease()
        {
            if (_handle != null)
            {
                _handle.Release();
            }
        }

        public void PlayAnimation(string animName)
        {
            _animation.Play(animName);
        }

        private void OnAttributeChange(IEventMessage message)
        {
            var msg = message as UnitAttributeChangeEvent;
            if (msg.InstanceId != _controller.InstanceId)
            {
                return;
            }

            if (msg.NumericId == Cfg.NumericId.Hp)
            {
                if (msg.OldValue > 0 && msg.NewValue <= 0)
                {
                    _animation.Play("XiaoMingAnim_Die");
                }
            }
        }
    }
}
