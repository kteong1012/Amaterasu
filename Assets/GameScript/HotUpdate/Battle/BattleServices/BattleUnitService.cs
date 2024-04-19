using Cysharp.Threading.Tasks;
using Game.Cfg;
using Game.Log;
using System;
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

        protected override async UniTask OnInit()
        {
            _instanceId = 1000;

            var unit1 = CreateBattleUnit(1001, 10, UnitCamp.Red);
            unit1.transform.position = new Vector3(20, unit1.transform.position.y, 20);
            var unit2 = CreateBattleUnit(1001, 10, UnitCamp.Blue);
            unit2.transform.position = new Vector3(-20, unit2.transform.position.y, -20);

            await UniTask.CompletedTask;
        }
        public UnitController CreateBattleUnit(int unitId, int level, UnitCamp unitCamp)
        {
            var unitData = _configService.TbUnitData.GetOrDefault(unitId);
            if (unitData == null)
            {
                GameLog.Error($"UnitData 表中没有找到Id为{unitId}的数据");
                return null;
            }
            var instanceId = ++_instanceId;
            var go = new GameObject();
            go.name = $"Unit_{unitData.Name}_{unitCamp}_{instanceId}";
            var battleUnit = go.GetOrAddComponent<CharacterUnit>();
            battleUnit.Init(instanceId, unitData, level, unitCamp);

            _battleUnits.Add(instanceId, battleUnit);
            return battleUnit;
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

        public UnitController GetNearestEnemy(UnitController controller)
        {
            var minDistance = float.MaxValue;
            UnitController nearestEnemy = null;
            foreach (var unit in _battleUnits.Values)
            {
                if (unit.Camp == controller.Camp)
                {
                    continue;
                }
                var distance = Vector3.Distance(unit.transform.position, controller.transform.position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    nearestEnemy = unit;
                }
            }
            return nearestEnemy;
        }
    }
}
