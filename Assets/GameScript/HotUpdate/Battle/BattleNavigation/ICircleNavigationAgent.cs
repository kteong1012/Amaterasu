using System.Numerics;

namespace Game
{
    public interface ICircleNavigationAgent
    {
        float Radius { get; }
        float DistanceTo { get; }
        Vector2 Position { get; }
        Vector2 TargetPosition { get; }
        float Speed { get; }
        bool Enable { get; }
    }
}
