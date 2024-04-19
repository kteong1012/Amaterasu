using Cysharp.Threading.Tasks;
using Game.Cfg;
using Game.Log;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    [GameService(GameServiceLifeSpan.Battle)]
    public class BattleUnitService : GameService
    {
        private int _instanceId;
        private Dictionary<int, UnitController> _battleUnits = new Dictionary<int, UnitController>();
        private ConfigService _configService;

        public override UniTask Init()
        {
            _instanceId = 1000;
            return base.Init();
        }
        public void CreateBattleUnit(int unitId, UnitCamp unitCamp)
        {
            var unitData = _configService.TbUnitData.GetOrDefault(unitId);
            if (unitData == null)
            {
                GameLog.Error($"UnitData 表中没有找到Id为{unitId}的数据");
                return;
            }
            var instanceId = ++_instanceId;
            var go = new GameObject();
            go.name = $"Unit_{unitData.Name}_{unitCamp}_{instanceId}";
            var battleUnit = go.GetOrAddComponent<UnitController>();
            battleUnit.Init(instanceId, unitData, unitCamp);

            _battleUnits.Add(instanceId, battleUnit);
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
