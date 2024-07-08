using Cysharp.Threading.Tasks;
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

        protected override void OnInit()
        {
            var unitData = _controller.UnitData;
            var handle = GameServices.ResourceService.LoadAssetSync<GameObject>(unitData.ModelPath);
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

        public void PlayAnimation(string animName, string queuedAnim = "")
        {
            _animation.Play(animName);
            if (!string.IsNullOrEmpty(queuedAnim))
            {
                _animation.PlayQueued(queuedAnim);
            }
        }

        public float GetAnimationClipDuration(string animName)
        {
            var clip = _animation.GetClip(animName);
            if (clip == null)
            {
                return 0;
            }
            return clip.length;
        }

        public async UniTask PlayAnimationAndWaitAsync(string animName)
        {
            _animation.Play(animName);
            await UniTask.WaitWhile(() => _animation.IsPlaying(animName));
        }

        public void ThrowModelAway(float destroyDelay = 0f)
        {
            _model.transform.SetParent(null);
            GameObject.Destroy(_model, destroyDelay);
        }
    }
}
