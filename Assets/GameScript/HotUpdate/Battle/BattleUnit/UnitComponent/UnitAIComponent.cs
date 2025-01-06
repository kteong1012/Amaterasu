using Game.Log;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class UnitAIComponent : UnitComponent
    {
        #region Fields & Properties
        public BattleUnitController battleUnit;

        private UnitAI _ai;
        private float? _lastActTime;
        #endregion

        #region Life Cycle
        protected override void OnInit()
        {
            battleUnit = _controller as BattleUnitController;
        }
        private void Update()
        {
            if (_ai != null)
            {
                var ACTITV = battleUnit.GetStatsValue(Cfg.NumericId.ACTITV);
                var ACTSPD = battleUnit.GetStatsValue(Cfg.NumericId.ACTSPD);
                var finalACTITV = BattleCalculator.CalculateFinalACTITV(ACTITV, ACTSPD);

                if (_lastActTime != null && Time.time - _lastActTime < finalACTITV)
                {
                    return;
                }
                _lastActTime = Time.time;

                _ai.Act(this);
            }
            else
            {
                _lastActTime = null;
            }
        }

        protected override void OnRelease()
        {
            _ai = null;
            _lastActTime = null;
        }
        #endregion

        #region Public Methods
        public void SetAI<T>() where T : UnitAI, new()
        {
            _ai = new T();
            _ai.Init(this);
        }
        #endregion
    }
}
