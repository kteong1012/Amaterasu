using Game.Cfg;
using Game.Cfg.Unit;
using log4net.Core;
using System.Collections.Generic;
using UniFramework.Event;
using Unity.VisualScripting.YamlDotNet.Core.Tokens;

namespace Game
{

    public class UnitAttributesComponent : UnitComponent
    {
        #region Fields & Properties
        private Dictionary<NumericId, NumericObject> _attributesMap = new Dictionary<NumericId, NumericObject>();
        private Dictionary<NumericId, NumericObject> _surplusAttributesMap = new Dictionary<NumericId, NumericObject>();
        #endregion

        #region Life Cycle
        protected override void OnInit()
        {
            var level = _controller.Level;
            var attributes = CalculateAttributes(level);
            _attributesMap = attributes;
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
                UnitAttributeChangeEvent.SendMsg(_controller.InstanceId, id, oldValue, numeric.GetValue());
            }
        }

        private NumericObject GetNumericObject(NumericId id)
        {
            if (_attributesMap.TryGetValue(id, out var numeric))
            {
                return numeric;
            }
            else
            {
                var newNumeric = new NumericObject(id);
                _attributesMap.Add(id, newNumeric);
                return newNumeric;
            }
        }

        public void Remove(NumericId id)
        {
            if (_attributesMap.ContainsKey(id))
            {
                _attributesMap.Remove(id);
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

        public void AddSurplusAttribute(NumericId id, int value, NumericValueType valueType)
        {
            if (_surplusAttributesMap == null)
            {
                _surplusAttributesMap = new Dictionary<NumericId, NumericObject>();
            }
            if (!_surplusAttributesMap.TryGetValue(id, out var numeric))
            {
                numeric = new NumericObject(id);
                _surplusAttributesMap.Add(id, numeric);
            }
            numeric.LinearAdd(valueType, value);
        }
        #endregion

        #region Private & Protected Methods
        /// <summary>
        /// 计算属性，只会在角色进入战斗时调用一次
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        private Dictionary<NumericId, NumericObject> CalculateAttributes(int level)
        {
            var attributes = new Dictionary<NumericId, NumericObject>();
            foreach (var attr in _controller.UnitData.AttributesX1000)
            {
                if (!attributes.TryGetValue(attr.Id, out var numeric))
                {
                    numeric = new NumericObject(attr.Id);
                    attributes.Add(attr.Id, numeric);
                }
                // 配置表中的属性值是实际值X1000
                var baseValue = NumberX1000.CreateFromX1000Value(attr.BaseValue.Value);
                var growValue = NumberX1000.CreateFromX1000Value(attr.GrowValue.Value);
                numeric.LinearAdd(attr.BaseValue.Type, baseValue);
                numeric.LinearAdd(attr.GrowValue.Type, growValue, level - 1);
            }

            if (_surplusAttributesMap != null)
            {
                foreach (var (id, numeric) in _surplusAttributesMap)
                {
                    if (!attributes.TryGetValue(id, out var attr))
                    {
                        attr = new NumericObject(id);
                        attributes.Add(id, attr);
                    }
                    attr.Base += numeric.Base;
                    attr.BaseAdd += numeric.BaseAdd;
                    attr.BaseMul += numeric.BaseMul;
                    attr.FinalAdd += numeric.FinalAdd;
                    attr.FinalMul += numeric.FinalMul;
                }
            }

            if (!attributes.ContainsKey(NumericId.Hp) && attributes.TryGetValue(NumericId.HpMax, out var hpMax))
            {
                var hp = new NumericObject(NumericId.Hp);
                hp.LinearAdd(NumericValueType.Base, hpMax.GetValue());
                attributes.Add(NumericId.Hp, hp);
            }
            if (!attributes.ContainsKey(NumericId.Mp) && attributes.TryGetValue(NumericId.MpMax, out var mpMax))
            {
                var mp = new NumericObject(NumericId.Mp);
                mp.LinearAdd(NumericValueType.Base, mpMax.GetValue());
                attributes.Add(NumericId.Mp, mp);
            }

            return attributes;
        }
        #endregion
    }

    #region Other Classes
    public class UnitAttributeChangeEvent : IEventMessage
    {
        public int InstanceId { get; }
        public NumericId NumericId { get; }
        public NumberX1000 OldValue { get; }
        public NumberX1000 NewValue { get; }

        public UnitAttributeChangeEvent(int instanceId, NumericId numericId, NumberX1000 oldValue, NumberX1000 newValue)
        {
            InstanceId = instanceId;
            NumericId = numericId;
            OldValue = oldValue;
            NewValue = newValue;
        }

        public static void SendMsg(int instanceId, NumericId numericId, NumberX1000 oldValue, NumberX1000 newValue)
        {
            var msg = new UnitAttributeChangeEvent(instanceId, numericId, oldValue, newValue);
            UniEvent.SendMessage(msg);
        }
    }
    #endregion
}