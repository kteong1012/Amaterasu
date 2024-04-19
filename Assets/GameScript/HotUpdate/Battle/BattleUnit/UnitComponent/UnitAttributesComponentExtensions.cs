using Game.Cfg;

namespace Game
{
    public static class UnitAttributesComponentExtensions
    {
        public static bool IsDead(this UnitAttributesComponent attributes)
        {
            return attributes.GetValue(NumericId.Hp) <= 0;
        }
    }
}