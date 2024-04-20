using UnityEngine;

namespace Game
{
    public class UnitAI_XiaoMing : UnitAI
    {
        private const float _stopDistanceToRangeRatio = 0.95f;
        private BattleUnitService _battleUnitService;
        private UnitAttributesComponent _attributesComponent;
        private UnitNavigationComponent _navigationComponent;
        private int _currentTargetId;
        private float _outOfRangeTimes = 1.5f;

        public override void Init(UnitAIComponent aIComponent)
        {
            _battleUnitService = GameEntry.Ins.GetService<BattleUnitService>();
            _attributesComponent = aIComponent.Controller.GetUnitComponent<UnitAttributesComponent>();
            _navigationComponent = aIComponent.Controller.GetUnitComponent<UnitNavigationComponent>();
        }
        public override void Tick(UnitAIComponent aIComponent)
        {
            var range = _attributesComponent.GetValue(Cfg.NumericId.AttackRange);
            _navigationComponent.StopDistance = range * _stopDistanceToRangeRatio;
            var currentTarget = _battleUnitService.GetBattleUnit(_currentTargetId);
            // 如果当前目标为空或者死亡, 重新选择目标
            if (currentTarget == null || currentTarget.GetUnitComponent<UnitAttributesComponent>().IsDead())
            {
                RepickTarget(aIComponent, range);
            }
            // 如果当前目标不为空, 且未死亡
            else
            {
                //如果当前目标在_outOfRangeTimes * range范围外, 重新选择目标, 否则追击或者攻击
                var distanceLogic = Vector3.Distance(aIComponent.Controller.LogicPosition, currentTarget.LogicPosition);
                if (distanceLogic > _outOfRangeTimes * range)
                {
                    RepickTarget(aIComponent, range);
                }
                else
                {
                    ChaseOrAttack(aIComponent.Controller, currentTarget, range);
                }
            }

        }

        private void ChaseOrAttack(UnitController selfUnit, UnitController targetUnit, NumberX1000 range)
        {
            if (selfUnit is not BattleUnitController battleUnitController)
            {
                return;
            }
            _currentTargetId = targetUnit.InstanceId;
            var distanceLogic = Vector3.Distance(selfUnit.LogicPosition, targetUnit.LogicPosition);
            if (distanceLogic <= range)
            {
                battleUnitController.NormalAttack(targetUnit);
            }
            else
            {
                battleUnitController.MoveTo(targetUnit.LogicPosition);
            }
        }

        private UnitController RepickTarget(UnitAIComponent aIComponent, float range)
        {
            // 选择最近的敌人
            UnitController currentTarget = _battleUnitService.GetNearestEnemy(aIComponent.Controller);
            if (currentTarget == null)
            {
                // 没有敌人，说明游戏结束, 原地不动
                _currentTargetId = 0;
                _navigationComponent.Destination = aIComponent.Controller.LogicPosition;
            }
            else
            {
                ChaseOrAttack(aIComponent.Controller, currentTarget, range);
            }

            return currentTarget;
        }
    }
}