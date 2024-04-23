using Game.Cfg;
using System;
using UniFramework.Event;
using UnityEngine;
using YooAsset;

namespace Game
{
    public class UnitUI3DComponent : UnitComponent
    {
        private UI3D_UnitHud _uiUnitHud;
        private BattleUnitController _battleUnit;
        private AssetHandle _handle;

        protected override void OnInit()
        {
            _handle = YooAssets.LoadAssetSync<GameObject>($"UI3D_UnitHud");
            var go = _handle.InstantiateSync(MainCamera.Instance.worldCanvasRoot);
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
            _battleUnit.OnStatsChangeEvent -= OnStatsChange;
            _uiUnitHud.gameObject.TryDestroy();
            _uiUnitHud = null;
            _handle.Release();
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
                        UI3D_DamageText.Create(_uiUnitHud.transform, $"-{damage.Ceil().ToIntegerString()}");
                    }
                    else
                    {
                        var heal = newValue - oldValue;
                        UI3D_DamageText.Create(_uiUnitHud.transform, $"+{heal.Ceil().ToIntegerString()}");
                    }
                }
            }
        }
    }
}
