using Game.Cfg;
using System;
using UnityEngine;

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

        public static NumberX1000 CalculateFinalDamage(NumberX1000 rawDamage, INumericGetter reader, out bool isCritical)
        {
            // 最终伤害 = 原始伤害乘区 * 暴击乘区

            // 计算暴击乘区
            var criticalRate = reader.GetValue(NumericId.CritRate);
            var criticalDamage = reader.GetValue(NumericId.CritDMG);
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
        /// 计算行动间隔，公式：基础行动间隔 / (行动速度 * 0.01) 
        /// </summary>
        /// <param name="ACTITV"></param>
        /// <param name="ACTSPD"></param>
        /// <returns></returns>
        public static NumberX1000 CalculateFinalACTITV(NumberX1000 ACTITV, NumberX1000 ACTSPD)
        {
            //修正后的行动速度
            var max = BattleConstants.MaxACTSPED;
            var min = BattleConstants.MinACTSPED;
            ACTSPD = Mathf.Clamp(ACTSPD, min, max);
            return ACTITV / (ACTSPD * NumberX1000.CreateFromX1000Value(10));
        }

        public static NumberX1000 CalculateNavigationStopDistance(UnitController self, UnitController target, NumberX1000 range)
        {
            //selfRadius + targetRadius + range
            var selfRadius = NumberX1000.CreateFromX1000Value(self.UnitData.RadiusX1000);
            var targetRadius = NumberX1000.CreateFromX1000Value(target.UnitData.RadiusX1000);
            return selfRadius + targetRadius + range;
        }

        public static bool IsInAttackRange(UnitController self, UnitController target, NumberX1000 range)
        {
            var distanceLogic = (NumberX1000)Vector3.Distance(self.LogicPosition, target.LogicPosition);
            var stopDistance = CalculateNavigationStopDistance(self, target, range);
            // 如果近似则视为相等，在攻击范围内
            if (distanceLogic.ApproximatelyTo(stopDistance))
            {
                return true;
            }
            return distanceLogic < stopDistance;
        }
    }
}
