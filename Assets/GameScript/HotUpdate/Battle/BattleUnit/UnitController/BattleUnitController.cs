using Game.Cfg;
using Game.Log;
using System;
using System.Collections.Generic;
using UniFramework.Event;
using UnityEngine;

namespace Game
{
    public class BattleUnitController : UnitController
    {
        #region Fields & Properties
        private UnitStatsComponent _statsComponent;
        private UnitModelComponent _modelComponent;
        private UnitNavigationComponent _navigationComponent;
        private UnitAIComponent _aiComponent;
        private UnitSkillComponent _skillComponent;
        private UnitUI3DComponent _unitHudComponent;

        public event StatsChangeHandler OnStatsChangeEvent
        {
            add { _statsComponent.onStatsChangeEvent += value; }
            remove { _statsComponent.onStatsChangeEvent -= value; }
        }
        #endregion

        #region Life Cycle

        protected override void OnInit()
        {
            _modelComponent = AddUnitComponent<UnitModelComponent>();
            _modelComponent.Init(this);

            _statsComponent = AddUnitComponent<UnitStatsComponent>();
            _statsComponent.Init(this);

            _navigationComponent = AddUnitComponent<UnitNavigationComponent>();
            _navigationComponent.Init(this);

            _aiComponent = AddUnitComponent<UnitAIComponent>();
            _aiComponent.Init(this);

            _skillComponent = AddUnitComponent<UnitSkillComponent>();
            _skillComponent.Init(this);

            _unitHudComponent = AddUnitComponent<UnitUI3DComponent>();
            _unitHudComponent.Init(this);

            _aiComponent.SetAI<UnitAI_XiaoMing>();
            _statsComponent.onStatsChangeEvent += OnStatsChange;
        }

        protected override void OnRelease()
        {
            _statsComponent.onStatsChangeEvent -= OnStatsChange;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// 普通攻击
        /// </summary>
        public void StartAct(UnitController target)
        {
            // 停止移动
            _navigationComponent.StopMove();
            // 释放技能
            _skillComponent.CastSkill(target);
        }

        public void MoveToPlace(Vector3 targetPosition, NumberX1000 stopDistance)
        {
            _navigationComponent.MoveToPlace(targetPosition, stopDistance);
            PlayAnimation("XiaoMingAnim_Move");
        }

        public void ChaseUnit(UnitController targetUnit, NumberX1000 stopDistance)
        {
            _navigationComponent.ChaseUnit(targetUnit, stopDistance);
            PlayAnimation("XiaoMingAnim_Move");
        }

        public void StopMove()
        {
            _navigationComponent.StopMove();
            PlayAnimation("XiaoMingAnim_Idle");
        }

        public void Die()
        {
            // 播放死亡动画
            var animationName = "XiaoMingAnim_Die";
            PlayAnimation(animationName);
            _modelComponent.ThrowModelAway(3f);

            // 从BattleUnitService移除自己
            var battleUnitService = GameEntry.Ins.GetService<BattleUnitService>();
            battleUnitService.DestroyUnit(InstanceId);
        }

        public void TakeDamage(float damage)
        {
            var hp = _statsComponent.GetValue(Cfg.NumericId.HP);
            var defense = _statsComponent.GetValue(Cfg.NumericId.DEF);

            var damageTaken = BattleCalculator.CalculateBaseDamage(damage, defense);
            var newHp = hp - damageTaken;
            _statsComponent.SetBase(Cfg.NumericId.HP, newHp);

            GameLog.Debug($"<color={Camp.GetColor().ToHex()}>{name} 受到伤害 {damageTaken}，剩余血量 {newHp}</color>");
        }
        #endregion

        #region Component Agent
        public NumberX1000 GetStatsValue(NumericId id)
        {
            return _statsComponent.GetValue(id);
        }

        public INumericGetter GetStatsGetter()
        {
            return _statsComponent;
        }

        public void PlayAnimation(string name)
        {
            _modelComponent.PlayAnimation(name);
        }
        #endregion

        #region Event Handlers
        private void OnStatsChange(NumericId id, NumberX1000 oldValue, NumberX1000 newValue)
        {
            // 血量变化
            if (id == NumericId.HP)
            {
                if (oldValue > 0 && newValue <= 0)
                {
                    Die();
                }
            }
        }
        #endregion
    }
}
