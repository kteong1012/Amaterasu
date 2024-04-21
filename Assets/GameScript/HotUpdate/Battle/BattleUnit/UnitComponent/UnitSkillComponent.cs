using Cysharp.Threading.Tasks;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using Game.Log;
using UnityEngine;

namespace Game
{
    public class UnitSkillComponent : UnitComponent
    {
        private float? _lastActTime;
        private BattleUnitController _battleUnit;
        private TweenerCore<Quaternion, Quaternion, NoOptions> _tweener;

        protected override void OnInit()
        {
            _battleUnit = _controller as BattleUnitController;
        }

        protected override void OnRelease()
        {
            _tweener?.Kill();
        }

        public void Act(UnitController target)
        {
            if (_battleUnit.IsDead())
            {
                return;
            }
            if (target == null || target is BattleUnitController battleUnit && battleUnit.IsDead())
            {
                return;
            }

            var ACTITV = _battleUnit.GetStatsValue(Cfg.NumericId.ACTITV).ToFloat();
            var ACTSPD = _battleUnit.GetStatsValue(Cfg.NumericId.ACTSPD).ToFloat();
            var finalACTITV = BattleCalculator.CalculateACTITV(ACTITV, ACTSPD);

            if (_lastActTime != null && Time.time - _lastActTime < finalACTITV)
            {
                return;
            }
            _lastActTime = Time.time;

            // 面向目标
            RotateToTarget(target);

            //暂时只有普通攻击
            // 播放攻击动画
            _battleUnit.PlayAnimation("XiaoMingAnim_Attack");

            if (target is BattleUnitController targetBattleUnit)
            {
                var attack = _battleUnit.GetStatsValue(Cfg.NumericId.ATK);
                var damage = BattleCalculator.CalculateFinalDamage(attack, _battleUnit.GetStatsGetter(), out var isCritical);
                GameLog.Debug($"<color={_controller.Camp.GetColor().ToHex()}>{name}发动普通攻击, 造成{damage}点伤害, 是否暴击: {isCritical}</color>");
                targetBattleUnit.TakeDamage(damage);
            }
        }

        public void StopAct()
        {
            _lastActTime = null;
        }

        private void RotateToTarget(UnitController target)
        {
            GameLog.Info("RotateToTarget");
            var targetPosition = target.LogicPosition;
            var direction = targetPosition - _battleUnit.LogicPosition;
            var angularSpeed = 360f;
            // 旋转时间 = 旋转角度 / 角速度
            var duration = Quaternion.Angle(_battleUnit.transform.rotation, Quaternion.LookRotation(direction)) / angularSpeed;
            _tweener?.Kill();
            _tweener = _battleUnit.transform.DORotateQuaternion(Quaternion.LookRotation(direction), duration);
        }
    }
}