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

        protected override async UniTask OnInit()
        {
            _instanceId = 1000;

            CollectUnitControllerTypes();

            var unit1 = CreateUnit(1001, 10, UnitCamp.Red);
            unit1.transform.position = new Vector3(20, unit1.transform.position.y, 20);
            var unit2 = CreateUnit(1001, 10, UnitCamp.Blue);
            unit2.transform.position = new Vector3(-20, unit2.transform.position.y, -20);
            unit2.GetUnitComponent<UnitStatsComponent>().LinearAdd(NumericId.ACTSPD, NumericValueType.BaseAdd, NumberX1000.One);
            await UniTask.CompletedTask;
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
