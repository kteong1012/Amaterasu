
namespace Game
{
    public interface INumericGetter
    {
        NumberX1000 GetBase(int id);
        NumberX1000 GetBaseAdd(int id);
        NumberX1000 GetBaseMul(int id);
        NumberX1000 GetFinalAdd(int id);
        NumberX1000 GetFinalMul(int id);
        NumberX1000 GetValue(int id);
    }
    public interface INumericSetter
    {
        void SetBase(int id, NumberX1000 value);
        void SetBaseAdd(int id, NumberX1000 value);
        void SetBaseMul(int id, NumberX1000 value);
        void SetFinalAdd(int id, NumberX1000 value);
        void SetFinalMul(int id, NumberX1000 value);
    }
}
