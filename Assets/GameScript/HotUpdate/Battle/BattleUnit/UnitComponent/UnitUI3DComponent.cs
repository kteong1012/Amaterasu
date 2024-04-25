using DG.Tweening;
using Game.Cfg;
using System;
using UniFramework.Event;
using UnityEngine;
using UnityEngine.Pool;
using YooAsset;

namespace Game
{
    public class UnitUI3DComponent : UnitComponent
    {
        private UI3D_UnitHud _uiUnitHud;
        private AssetHandle _unitHudHandle;

        private BattleUnitController _battleUnit;

        private YooAssetGameObjectPool _damageTextPool;

        protected override void OnInit()
        {
            _unitHudHandle = YooAssets.LoadAssetSync<GameObject>($"UI3D_UnitHud");
            _damageTextPool = new YooAssetGameObjectPool("UI3D_DamageText");
            var go = _unitHudHandle.InstantiateSync(MainCamera.Instance.worldCanvasRoot);
            go.name = $"UI3D_UnitHud_{_controller.name}";
            _uiUnitHud = go.GetComponent<UI3D_UnitHud>();

            _battleUnit = _controller as BattleUnitController;
            _battleUnit.OnStatsChangeEvent += OnStatsChange;
            _uiUnitHud.SetHP(_battleUnit.GetStatsValue(NumericId.HP), _battleUnit.GetStatsValue(NumericId.MaxHP));
        }

        private void Update()
        {
            if (_uiUnitHud != null)
            {
                _uiUnitHud.transform.localPosition = MainCamera.Instance.Camera.WorldToScreenPoint(_battleUnit.transform.position) + new Vector3(0, 100, 0);
            }
        }

        protected override void OnRelease()
        {
            _damageTextPool.Dispose();
            _battleUnit.OnStatsChangeEvent -= OnStatsChange;
            _uiUnitHud.gameObject.TryDestroy();
            _uiUnitHud = null;
            _unitHudHandle.Release();
        }

        private void OnStatsChange(NumericId id, NumberX1000 oldValue, NumberX1000 newValue)
        {
            if (id == NumericId.HP || id == NumericId.MaxHP)
            {
                _uiUnitHud.SetHP(_battleUnit.GetStatsValue(NumericId.HP), _battleUnit.GetStatsValue(NumericId.MaxHP));

                if (id == NumericId.HP)
                {
                    if (oldValue > newValue)
                    {
                        var damage = oldValue - newValue;
                        var content = $"-{damage.Ceil().ToIntegerString()}";
                        ShowDamageText(_uiUnitHud.transform, content);
                    }
                    else
                    {
                        var heal = newValue - oldValue;
                        var content = $"+{heal.Ceil().ToIntegerString()}";
                        ShowDamageText(_uiUnitHud.transform, content);
                    }
                }
            }
        }

        private async void ShowDamageText(Transform parent, string content)
        {
            var text = _damageTextPool.Get<UI3D_DamageText>(parent);
            await text.StartFloating(content);
            _damageTextPool.Release(text.gameObject);
        }
    }
}
