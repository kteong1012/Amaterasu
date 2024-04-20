using Game.Cfg;
using Game.Log;
using System;
using UniFramework.Event;
using UnityEngine;

namespace Game
{
    public class BattleUnitController : UnitController
    {
        private UnitAttributesComponent _attributesComponent;
        private UnitNavigationComponent _navigationComponent;
        private UnitSkillComponent _skillComponent;



        protected override void AddComponents()
        {
            AddUnitComponent<UnitModelComponent>();
            AddUnitComponent<UnitAttributesComponent>();
            AddUnitComponent<UnitNavigationComponent>();
            AddUnitComponent<UnitAIComponent>();
            AddUnitComponent<UnitSkillComponent>();
        }

        protected override void OnInit()
        {
            _attributesComponent = GetComponent<UnitAttributesComponent>();
            _navigationComponent = GetComponent<UnitNavigationComponent>();
            _skillComponent = GetComponent<UnitSkillComponent>();
            GetUnitComponent<UnitAIComponent>().SetAI<UnitAI_XiaoMing>();

            _attributesComponent.onAttributeChange += OnAttributeChange;
        }

        /// <summary>
        /// 普通攻击
        /// </summary>
        public void NormalAttack(UnitController target)
        {
            _skillComponent.NormalAttack(target);
        }

        public void MoveTo(Vector3 targetPosition)
        {
            var navigationComponent = GetUnitComponent<UnitNavigationComponent>();
            var modelComponent = GetUnitComponent<UnitModelComponent>();

            navigationComponent.IsStopped = false;
            navigationComponent.Destination = targetPosition;
            modelComponent.PlayAnimation("XiaoMingAnim_Move");
        }

        public void StopMove()
        {
            var navigationComponent = GetUnitComponent<UnitNavigationComponent>();
            var modelComponent = GetUnitComponent<UnitModelComponent>();

            navigationComponent.IsStopped = true;
            modelComponent.PlayAnimation("XiaoMingAnim_Idle");
        }

        public void Die()
        {
            // 播放死亡动画
            var modelComponent = GetUnitComponent<UnitModelComponent>();
            var animationName = "XiaoMingAnim_Die";
            modelComponent.PlayAnimation(animationName);
            modelComponent.ThrowModelAway(modelComponent.GetAnimationClipDuration(animationName));

            // 从BattleUnitService移除自己
            var battleUnitService = GameEntry.Ins.GetService<BattleUnitService>();
            battleUnitService.DestroyBattleUnit(InstanceId);
        }

        public void TakeDamage(float damage)
        {
            var hp = _attributesComponent.GetValue(Cfg.NumericId.Hp);
            var defense = _attributesComponent.GetValue(Cfg.NumericId.Defense);

            var damageTaken = BattleCalculator.CalculateBaseDamage(damage, defense);
            var newHp = hp - damageTaken;
            _attributesComponent.SetBase(Cfg.NumericId.Hp, newHp);

            GameLog.Debug($"<color={Camp.GetColor().ToHex()}>{name} 受到伤害 {damageTaken}，剩余血量 {newHp}</color>");
        }

        private void OnAttributeChange(NumericId id, NumberX1000 oldValue, NumberX1000 newValue)
        {
            // 血量变化
            if (id == Cfg.NumericId.Hp)
            {
                if (oldValue > 0 && newValue <= 0)
                {
                    Die();
                }
            }
        }
    }
}
