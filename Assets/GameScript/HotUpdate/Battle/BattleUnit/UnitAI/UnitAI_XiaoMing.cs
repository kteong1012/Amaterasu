using Game.Cfg;
using UnityEngine;

namespace Game
{
    public class UnitAI_XiaoMing : UnitAI
    {
        private BattleUnitService _battleUnitService;
        private int _currentTargetId;

        public override void Init(UnitAIComponent aIComponent)
        {
            _battleUnitService = GameService<BattleUnitService>.Instance;
        }
        public override void Act(UnitAIComponent aIComponent)
        {
            var battleUnitController = aIComponent.battleUnit;
            //TODO 计算一番拿到行动范围,现在暂定是一个近战普通攻击的距离0.5,所有技能攻击距离都不能小于这个距离
            var range = NumberX1000.CreateFromX1000Value(500);

            var currentTarget = _battleUnitService.GetUnit(_currentTargetId);
            // 如果当前目标为空或者死亡, 重新选择目标
            if (currentTarget == null || currentTarget is BattleUnitController battleUnit && battleUnit.IsDead())
            {
                currentTarget = _battleUnitService.GetNearestEnemy(battleUnitController);
            }
            if (currentTarget != null)
            {
                _currentTargetId = currentTarget.InstanceId;
                ChaseOrAttack(battleUnitController, currentTarget, range);
            }
        }

        private void ChaseOrAttack(BattleUnitController selfUnit, UnitController targetUnit, NumberX1000 range)
        {
            if (BattleCalculator.IsInAttackRange(selfUnit, targetUnit, range))
            {
                selfUnit.StartAct(targetUnit);
            }
            else
            {
                var stopDistance = BattleCalculator.CalculateNavigationStopDistance(selfUnit, targetUnit, range);
                selfUnit.ChaseUnit(targetUnit, stopDistance);
            }
        }
    }
}