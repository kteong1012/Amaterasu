using Cysharp.Threading.Tasks;
using System.Collections.Generic;

namespace Game
{
    [GameService(GameServiceLifeSpan.Battle)]
    public class BattleUnitService : GameService
    {
        private int _unitId;
        private Dictionary<int, UnitController> _battleUnits = new Dictionary<int, UnitController>();

        public override UniTask Init()
        {
            _unitId = 1000;
            return base.Init();
        }
        public void AddBattleUnit(UnitController battleUnit)
        {
            var unitId = ++_unitId;
            battleUnit.UnitId = unitId;
            _battleUnits.Add(unitId, battleUnit);
        }
        public void RemoveBattleUnit(int unitId)
        {
            _battleUnits.Remove(unitId);
        }

        public UnitController GetBattleUnit(int unitId)
        {
            if (_battleUnits.TryGetValue(unitId, out var battleUnit))
            {
                return battleUnit;
            }
            return null;
        }

        public IEnumerable<UnitController> GetBattleUnits()
        {
            return _battleUnits.Values;
        }
    }
}
