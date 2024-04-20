using Game.Cfg;
using System;

namespace Game
{
    public static class BattleCalculator
    {
        /// <summary>
        /// 承伤计算, 伤害减免公式：1 - 0.06 * 防御力 / (1 + 0.06 * |防御力|)
        /// </summary>
        /// <param name="rawDamage"></param>
        /// <param name="defense"></param>
        /// <returns></returns>
        public static NumberX1000 CalculateBaseDamage(NumberX1000 rawDamage, NumberX1000 defense)
        {
            NumberX1000 damageReduction = 1f - 0.06f * defense / (1f + 0.06f * Math.Abs(defense));
            NumberX1000 damageTaken = rawDamage * damageReduction;
            return damageTaken;
        }

        public static NumberX1000 CalculateFinalDamage(NumberX1000 rawDamage, INumericReader reader, out bool isCritical)
        {
            // 最终伤害 = 原始伤害乘区 * 暴击乘区

            // 计算暴击乘区
            var criticalRate = reader.GetValue(NumericId.CriticalRate);
            var criticalDamage = reader.GetValue(NumericId.CriticalDamage);
            var criticalMultiArea = CalculateCriticalMultiArea(criticalRate, criticalDamage, out isCritical);

            // 计算最终伤害
            var finalDamage = rawDamage * criticalMultiArea;
            return finalDamage;
        }

        /// <summary>
        /// 暴击乘区，公式：暴击伤害 = 暴击 ? (1 + 暴击伤害) : 1
        /// </summary>
        /// <param name="criticalRate"></param>
        /// <param name="criticalDamage"></param>
        /// <returns></returns>
        public static NumberX1000 CalculateCriticalMultiArea(NumberX1000 criticalRate, NumberX1000 criticalDamage, out bool isCritical)
        {
            var randomZeroToOne = NumberX1000.CreateFromX1000Value(UnityEngine.Random.Range(0, 1000));
            isCritical = randomZeroToOne < criticalRate;
            return isCritical ? NumberX1000.One + criticalDamage : NumberX1000.One;
        }

        /// <summary>
        /// 计算攻击间隔，公式：基础攻击间隔 / (攻击速度 * 0.01) 
        /// </summary>
        /// <param name="baseInterval"></param>
        /// <param name="attackSpeed"></param>
        /// <returns></returns>
        public static float CalculateAttackInterval(float baseInterval, float attackSpeed)
        {
            return baseInterval / (attackSpeed * 0.01f);
        }
    }
}
