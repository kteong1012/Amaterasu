using Game.Cfg;
using Game.Log;
using System.Collections.Generic;
using UniFramework.Event;

namespace Game
{
    public class UnitStatsComponent : UnitComponent, INumericGetter, INumericSetter
    {
        #region Fields & Properties

        private Dictionary<NumericId, NumericObject> _statsMap;

        public event StatsChangeHandler onStatsChangeEvent;

        #endregion

        #region Life Cycle

        protected override void OnInit()
        {
            var level = _controller.Level;
            _statsMap = InitializeStats(level);
            GameLog.Debug($"{name}，等级：{level}，属性初始化完成：\n{GetStatsListString(_statsMap)}");
        }

        protected override void OnRelease()
        {
            onStatsChangeEvent = null;
        }

        #endregion

        #region Public Methods

        public NumberX1000 GetValue(NumericId id)
        {
            return GetNumericObject(id).GetValue();
        }

        private void SetNumericField(NumericId id, NumericValueType type, NumberX1000 value)
        {
            var numeric = GetNumericObject(id);
            var changed = false;
            var oldValue = numeric.GetValue();
            switch (type)
            {
                case NumericValueType.Base:
                    if (numeric.Base != value)
                    {
                        numeric.Base = value;
                        changed = true;
                    }

                    break;
                case NumericValueType.BaseAdd:
                    if (numeric.BaseAdd != value)
                    {
                        numeric.BaseAdd = value;
                        changed = true;
                    }

                    break;
                case NumericValueType.BaseMul:
                    if (numeric.BaseMul != value)
                    {
                        numeric.BaseMul = value;
                        changed = true;
                    }

                    break;
                case NumericValueType.FinalAdd:
                    if (numeric.FinalAdd != value)
                    {
                        numeric.FinalAdd = value;
                        changed = true;
                    }

                    break;
                case NumericValueType.FinalMul:
                    if (numeric.FinalMul != value)
                    {
                        numeric.FinalMul = value;
                        changed = true;
                    }

                    break;
                default:
                    break;
            }

            if (changed)
            {
                UnitStatsChangeEvent.SendMsg(_controller.InstanceId, id, oldValue, numeric.GetValue());
                onStatsChangeEvent?.Invoke(id, oldValue, numeric.GetValue());
            }
        }

        private NumericObject GetNumericObject(NumericId id)
        {
            if (_statsMap.TryGetValue(id, out var numeric))
            {
                return numeric;
            }
            else
            {
                var newNumeric = new NumericObject((int)id);
                _statsMap.Add(id, newNumeric);
                return newNumeric;
            }
        }

        public void Remove(NumericId id)
        {
            if (_statsMap.ContainsKey(id))
            {
                _statsMap.Remove(id);
            }
        }

        public void SetBase(NumericId id, NumberX1000 value)
        {
            SetNumericField(id, NumericValueType.Base, value);
        }

        public void SetBaseAdd(NumericId id, NumberX1000 value)
        {
            SetNumericField(id, NumericValueType.BaseAdd, value);
        }

        public void SetBaseMul(NumericId id, NumberX1000 value)
        {
            SetNumericField(id, NumericValueType.BaseMul, value);
        }

        public void SetFinalAdd(NumericId id, NumberX1000 value)
        {
            SetNumericField(id, NumericValueType.FinalAdd, value);
        }

        public void SetFinalMul(NumericId id, NumberX1000 value)
        {
            SetNumericField(id, NumericValueType.FinalMul, value);
        }

        public void LinearAdd(NumericId id, NumericValueType valueType, NumberX1000 addValue, int times = 1)
        {
            var numeric = GetNumericObject(id);
            numeric.LinearAdd(valueType, addValue, times);
        }

        public NumberX1000 GetBase(NumericId id)
        {
            return GetNumericObject(id).Base;
        }

        public NumberX1000 GetBaseAdd(NumericId id)
        {
            return GetNumericObject(id).BaseAdd;
        }

        public NumberX1000 GetBaseMul(NumericId id)
        {
            return GetNumericObject(id).BaseMul;
        }

        public NumberX1000 GetFinalAdd(NumericId id)
        {
            return GetNumericObject(id).FinalAdd;
        }

        public NumberX1000 GetFinalMul(NumericId id)
        {
            return GetNumericObject(id).FinalMul;
        }

        #endregion

        #region Private & Protected Methods

        NumberX1000 INumericGetter.GetBase(int id)
        {
            return GetBase((NumericId)id);
        }

        NumberX1000 INumericGetter.GetBaseAdd(int id)
        {
            return GetBaseAdd((NumericId)id);
        }

        NumberX1000 INumericGetter.GetBaseMul(int id)
        {
            return GetBaseMul((NumericId)id);
        }

        NumberX1000 INumericGetter.GetFinalAdd(int id)
        {
            return GetFinalAdd((NumericId)id);
        }

        NumberX1000 INumericGetter.GetFinalMul(int id)
        {
            return GetFinalMul((NumericId)id);
        }
        
        NumberX1000 INumericGetter.GetValue(int id)
        {
            return GetValue((NumericId)id);
        }

        void INumericSetter.SetBase(int id, NumberX1000 numberX1000)
        {
            SetBase((NumericId)id, numberX1000);
        }
        
        void INumericSetter.SetBaseAdd(int id, NumberX1000 numberX1000)
        {
            SetBaseAdd((NumericId)id, numberX1000);
        }
        
        void INumericSetter.SetBaseMul(int id, NumberX1000 numberX1000)
        {
            SetBaseMul((NumericId)id, numberX1000);
        }
        
        void INumericSetter.SetFinalAdd(int id, NumberX1000 numberX1000)
        {
            SetFinalAdd((NumericId)id, numberX1000);
        }
        
        void INumericSetter.SetFinalMul(int id, NumberX1000 numberX1000)
        {
            SetFinalMul((NumericId)id, numberX1000);
        }

        /// <summary>
        /// 计算属性，只会在角色进入战斗时调用一次
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        private Dictionary<NumericId, NumericObject> InitializeStats(int level)
        {
            var stats = new Dictionary<NumericId, NumericObject>();
            foreach (var (id, attr) in _controller.UnitData.BaseStatsX1000)
            {
                if (!stats.TryGetValue(id, out var numeric))
                {
                    numeric = new NumericObject((int)id);
                    stats.Add(id, numeric);
                }

                var baseValue = NumberX1000.CreateFromX1000Value(attr);
                numeric.LinearAdd(NumericValueType.Base, baseValue);
            }

            foreach (var (id, attr) in _controller.UnitData.LevelGrowthStatsX1000)
            {
                if (!stats.TryGetValue(id, out var numeric))
                {
                    numeric = new NumericObject((int)id);
                    stats.Add(id, numeric);
                }

                var growValue = NumberX1000.CreateFromX1000Value(attr);
                numeric.LinearAdd(NumericValueType.Base, growValue, level - 1);
            }

            // 初始化HP,一般是MaxHP的值
            if (!stats.ContainsKey(NumericId.HP) && stats.TryGetValue(NumericId.MaxHP, out var hpMax))
            {
                var hp = new NumericObject((int)NumericId.HP);
                hp.LinearAdd(NumericValueType.Base, hpMax.GetValue());
                stats.Add(NumericId.HP, hp);
            }

            // 初始化Energy，一般是MaxEnergy的一半
            if (!stats.ContainsKey(NumericId.Energy) && stats.TryGetValue(NumericId.MaxEnergy, out var energyMax))
            {
                var energy = new NumericObject((int)NumericId.Energy);
                energy.LinearAdd(NumericValueType.Base, energyMax.GetValue() / 2);
                stats.Add(NumericId.Energy, energy);
            }

            return stats;
        }

        private static string GetStatsListString(Dictionary<NumericId, NumericObject> stats)
        {
            var sb = new System.Text.StringBuilder();
            foreach (var attr in stats.Values)
            {
                sb.AppendLine($"{attr.Id}: {attr.GetValue()}");
            }

            return sb.ToString();
        }

        #endregion
    }

    #region Other Classes

    public class UnitStatsChangeEvent : IEventMessage
    {
        public int InstanceId { get; }
        public NumericId NumericId { get; }
        public NumberX1000 OldValue { get; }
        public NumberX1000 NewValue { get; }

        public UnitStatsChangeEvent(int instanceId, NumericId numericId, NumberX1000 oldValue, NumberX1000 newValue)
        {
            InstanceId = instanceId;
            NumericId = numericId;
            OldValue = oldValue;
            NewValue = newValue;
        }

        public static void SendMsg(int instanceId, NumericId numericId, NumberX1000 oldValue, NumberX1000 newValue)
        {
            var msg = new UnitStatsChangeEvent(instanceId, numericId, oldValue, newValue);
            UniEvent.SendMessage(msg);
        }
    }

    public delegate void StatsChangeHandler(NumericId id, NumberX1000 oldValue, NumberX1000 newValue);

    #endregion
}