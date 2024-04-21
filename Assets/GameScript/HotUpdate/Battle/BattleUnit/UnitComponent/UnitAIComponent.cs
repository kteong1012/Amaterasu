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
                _ai.Tick(this);
            }
        }

        protected override void OnRelease()
        {
            _ai = null;
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
