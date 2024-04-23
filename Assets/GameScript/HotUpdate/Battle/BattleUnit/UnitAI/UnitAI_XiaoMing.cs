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
            _battleUnitService = GameEntry.Ins.GetService<BattleUnitService>();
        }
        public override void Act(UnitAIComponent aIComponent)
        {
            var battleUnitController = aIComponent.battleUnit;
            //TODO 计算一番拿到行动范围,现在暂定是一个近战普通攻击的距离0
            var range = NumberX1000.Zero;

            var currentTarget = _battleUnitService.GetUnit(_currentTargetId);
            // 如果当前目标为空或者死亡, 重新选择目标
            if (currentTarget == null || currentTarget is BattleUnitController battleUnit && battleUnit.IsDead())
            {
                currentTarget = _battleUnitService.GetNearestEnemy(battleUnitController);
            }
            if (currentTarget != null)
            {
                _currentTargetId = currentTarget.InstanceId;
                // 血量低于50%时,逃跑
                if (battleUnitController.GetStatsValue(NumericId.HP) < battleUnitController.GetStatsValue(NumericId.MaxHP) / 2)
                {
                    Escape(battleUnitController, currentTarget);
                }
                else
                {
                    ChaseOrAttack(battleUnitController, currentTarget, range);
                }
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

        public void Escape(BattleUnitController selfUnit, UnitController targetUnit)
        {
            // move distance equals to move speed * actitv
            var moveSpeed = selfUnit.GetStatsValue(NumericId.MoveSPD);
            var ACTITV = selfUnit.GetStatsValue(NumericId.ACTITV);
            var ACTSPD = selfUnit.GetStatsValue(NumericId.ACTSPD);
            var finalACTITV = BattleCalculator.CalculateFinalACTITV(ACTITV, ACTSPD);
            var moveDistance = moveSpeed * finalACTITV;

            var direction = selfUnit.transform.position - targetUnit.transform.position;
            var targetPosition = selfUnit.transform.position + direction.normalized * moveDistance;
            selfUnit.MoveToPlace(targetPosition, NumberX1000.Zero);
        }
    }
}