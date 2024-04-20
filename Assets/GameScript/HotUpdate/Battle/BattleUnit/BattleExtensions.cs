using UnityEngine;

namespace Game
{
    public static class BattleExtensions
    {
        public static Vector3 ToLogic(this Vector3 vector)
        {
            return vector / BattleConstants.BattleSceneLogicToSceneScale;
        }

        public static Vector3 ToScene(this Vector3 vector)
        {
            return vector * BattleConstants.BattleSceneLogicToSceneScale;
        }

        public static NumberX1000 ToLogic(this NumberX1000 number)
        {
            return number / BattleConstants.BattleSceneLogicToSceneScale;
        }

        public static NumberX1000 ToScene(this NumberX1000 number)
        {
            return number * BattleConstants.BattleSceneLogicToSceneScale;
        }

        public static NumberX1000 ToLogic(this float number)
        {
            return ((NumberX1000)number).ToLogic();
        }

        public static NumberX1000 ToScene(this float number)
        {
            return ((NumberX1000)number).ToScene();
        }

        public static Color GetColor(this UnitCamp camp)
        {
            switch (camp)
            {
                case UnitCamp.Blue:
                    return Color.cyan;
                case UnitCamp.Red:
                    return Color.red;
                default:
                    return Color.white;
            }
        }
    }
}
