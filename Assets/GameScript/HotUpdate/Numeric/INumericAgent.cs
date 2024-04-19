using Game.Cfg;

namespace Game
{
    public interface INumericReader
    {
        NumberX1000 GetBase(NumericId id);
        NumberX1000 GetBaseAdd(NumericId id);
        NumberX1000 GetBaseMul(NumericId id);
        NumberX1000 GetFinalAdd(NumericId id);
        NumberX1000 GetFinalMul(NumericId id);
        NumberX1000 GetValue(NumericId id);
    }
    public interface INumericWriter
    {
        void SetBase(NumericId id, NumberX1000 value);
        void SetBaseAdd(NumericId id, NumberX1000 value);
        void SetBaseMul(NumericId id, NumberX1000 value);
        void SetFinalAdd(NumericId id, NumberX1000 value);
        void SetFinalMul(NumericId id, NumberX1000 value);
    }
}
