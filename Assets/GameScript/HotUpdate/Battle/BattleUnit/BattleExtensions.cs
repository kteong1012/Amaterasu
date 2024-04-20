using UnityEngine;

namespace Game
{
    public static class BattleExtensions
    {
        public static Vector2 XZ(this Vector3 v)
        {
            return new Vector2(v.x, v.z);
        }

        public static Vector3 ToScenePoint(this Vector2 v)
        {
            return new Vector3(v.x, 0, v.y);
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
