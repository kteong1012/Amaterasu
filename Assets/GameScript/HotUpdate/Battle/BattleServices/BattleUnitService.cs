using Cysharp.Threading.Tasks;
using Game.Cfg;
using Game.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using YIUIFramework;

namespace Game
{
    [GameService(GameServiceLifeSpan.Battle)]
    public class BattleUnitService : GameService
    {
        private Dictionary<string, Type> _unitControllerTypeMaps = new Dictionary<string, Type>();
        private int _instanceId;
        private Dictionary<int, UnitController> _Units = new Dictionary<int, UnitController>();
        private ConfigService _configService;

        protected override async UniTask Awake()
        {
            _instanceId = 1000;

            CollectUnitControllerTypes();
            await UniTask.CompletedTask;
        }

        override protected void OnDestroy()
        {
            DestroyAllUnits();
        }

        public UnitController CreateUnit(int unitId, int level, UnitCamp unitCamp)
        {
            var unitData = _configService.TbUnitData.GetOrDefault(unitId);
            if (unitData == null)
            {
                GameLog.Error($"UnitData 表中没有找到Id为{unitId}的数据");
                return null;
            }
            var controllerType = GetUnitControllerType(unitData.ControllerName);
            if (controllerType == null)
            {
                GameLog.Error($"没有找到名为{unitData.ControllerName}的UnitController");
                return null;
            }
            var instanceId = ++_instanceId;
            var go = new GameObject();
            go.name = $"Unit_{unitData.Name}_{unitCamp}_{instanceId}";
            var unit = go.GetOrAddComponent(controllerType) as UnitController;
            unit.Init(instanceId, unitData, level, unitCamp);

            _Units.Add(instanceId, unit);
            return unit;
        }
        public void DestroyUnit(int unitId)
        {
            if (_Units.TryGetValue(unitId, out var unit))
            {
                unit.Release();
                UnityEngine.Object.Destroy(unit.gameObject);
                _Units.Remove(unitId);
            }
            // 若其中一方没有单位了，结束战斗
            var groups = _Units.Values.GroupBy(unit => unit.Camp);
            if (groups.Count() == 1)
            {
                GameLog.Info($"战斗结束，胜利方为{groups.First().Key}");
                var winCamp = groups.First().Key;
                BattleEvent.BattleEndEvent.SendMsg(winCamp);
            }
        }

        public void DestroyAllUnits()
        {
            foreach (var unit in _Units.Values)
            {
                unit.Release();
                unit.gameObject.TryDestroy();
            }
            _Units.Clear();
        }

        public UnitController GetUnit(int unitId)
        {
            if (_Units.TryGetValue(unitId, out var unit))
            {
                return unit;
            }
            return null;
        }

        public IEnumerable<UnitController> GetUnits()
        {
            return _Units.Values;
        }

        public UnitController GetNearestEnemy(UnitController controller)
        {
            float Selector(UnitController unit)
            {
                var distance = Vector3.Distance(unit.LogicPosition, controller.LogicPosition);
                var targetRadius = NumberX1000.CreateFromX1000Value(unit.UnitData.RadiusX1000);
                var distanceMinusRadius = distance - targetRadius;
                return distanceMinusRadius;
            }

            return _Units.Values.Where(unit => unit.Camp != controller.Camp)
                .OrderBy(Selector)
                .FirstOrDefault();
        }

        private void CollectUnitControllerTypes()
        {
            var types = TypeManager.Instance.GetTypes();
            foreach (var type in types)
            {
                if (!type.IsAbstract && type.IsSubclassOf(typeof(UnitController)))
                {
                    var name = type.Name;
                    _unitControllerTypeMaps.Add(name, type);
                }
            }
        }

        private Type GetUnitControllerType(string name)
        {
            if (_unitControllerTypeMaps.TryGetValue(name, out var type))
            {
                return type;
            }
            return null;
        }
    }
}
