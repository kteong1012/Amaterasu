using Game.Cfg;

namespace Game
{
    public static class UnitExtensions
    {
        public static bool IsDead(this BattleUnitController battleUnit)
        {
            return battleUnit.GetStatsValue(NumericId.HP) <= 0;
        }
    }
}