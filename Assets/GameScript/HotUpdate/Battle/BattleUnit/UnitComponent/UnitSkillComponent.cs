using Game.Log;
using UnityEngine;

namespace Game
{
    public class UnitSkillComponent : UnitComponent
    {
        private UnitAttributesComponent _attributesComponent;
        private UnitNavigationComponent _navigationComponent;
        private UnitModelComponent _modelComponent;

        private float? _lastAttackTime;

        protected override void OnInit()
        {
            _attributesComponent = _controller.GetComponent<UnitAttributesComponent>();
            _navigationComponent = _controller.GetComponent<UnitNavigationComponent>();
            _modelComponent = _controller.GetComponent<UnitModelComponent>();
        }

        public void NormalAttack(UnitController target)
        {
            var baseAttackInterval = _attributesComponent.GetValue(Cfg.NumericId.AttackInterval).ToFloat();
            var attackSpeed = _attributesComponent.GetValue(Cfg.NumericId.AttackSpeed).ToFloat();
            var finalAttackInterval = BattleCalculator.CalculateAttackInterval(baseAttackInterval, attackSpeed);
            _navigationComponent.RotateTo(target.LogicPosition);

            if (_lastAttackTime != null && Time.time - _lastAttackTime < finalAttackInterval)
            {
                return;
            }
            _lastAttackTime = Time.time;
            _modelComponent.PlayAnimation("XiaoMingAnim_Attack");

            if (target is BattleUnitController targetBattleUnit)
            {
                var attack = _attributesComponent.GetValue(Cfg.NumericId.Attack);
                var damage = BattleCalculator.CalculateFinalDamage(attack, _attributesComponent, out var isCritical);
                //GameLog.Debug($"{name}发动普通攻击, 造成{damage}点伤害, 是否暴击: {isCritical}");
                GameLog.Debug($"<color={_controller.Camp.GetColor().ToHex()}>{name}发动普通攻击, 造成{damage}点伤害, 是否暴击: {isCritical}</color>");
                targetBattleUnit.TakeDamage(damage);
            }
        }
    }
}