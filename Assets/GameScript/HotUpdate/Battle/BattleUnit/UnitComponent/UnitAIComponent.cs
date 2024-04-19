using Game.Log;
using System.Collections.Generic;

namespace Game
{
    public class UnitAIComponent : UnitComponent
    {
        #region Fields & Properties
        public Dictionary<string, object> BlackBoard = new Dictionary<string, object>();
        private UnitAI _ai;

        public UnitController Controller => _controller;
        #endregion

        #region Life Cycle
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
            BlackBoard.Clear();
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
