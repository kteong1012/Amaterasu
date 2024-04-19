using Game.Cfg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class NumericObject
    {
        public NumericId Id { get; set; }
        public NumberX1000 Base { get; set; }
        public NumberX1000 BaseAdd { get; set; }
        public NumberX1000 BaseMul { get; set; }
        public NumberX1000 FinalAdd { get; set; }
        public NumberX1000 FinalMul { get; set; }

        public NumericObject(NumericId id)
        {
            Id = id;
        }

        public NumberX1000 GetValue()
        {
            return Base * (NumberX1000.One + BaseAdd) * (NumberX1000.One + BaseMul) + FinalAdd + FinalMul;
        }

        public void LinearAdd(NumericValueType type, float value, int times = 1)
        {
            switch (type)
            {
                case NumericValueType.Base:
                    Base += value * times;
                    break;
                case NumericValueType.BaseAdd:
                    BaseAdd += value * times;
                    break;
                case NumericValueType.BaseMul:
                    BaseMul += value * times;
                    break;
                case NumericValueType.FinalAdd:
                    FinalAdd += value * times;
                    break;
                case NumericValueType.FinalMul:
                    FinalMul += value * times;
                    break;
                default:
                    break;
            }
        }

        public override string ToString()
        {
            return $"Id: {Id}, Base: {Base}, BaseAdd: {BaseAdd}, BaseMul: {BaseMul}, FinalAdd: {FinalAdd}, FinalMul: {FinalMul}";
        }
    }
}
