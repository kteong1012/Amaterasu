using Cysharp.Threading.Tasks;
using Game.Log;
using System;
using System.Reflection;

namespace Game
{
    public class GameServiceAttribute : Attribute
    {
        public GameServiceLifeSpan LifeSpan { get; }

        public GameServiceAttribute(GameServiceLifeSpan lifeSpan)
        {
            LifeSpan = lifeSpan;
        }
    }
    public abstract class GameService
    {
        private GameServiceLifeSpan _lifeSpan => GetType().GetCustomAttribute<GameServiceAttribute>().LifeSpan;
        public virtual UniTask Init()
        {
            GameLog.Debug($"GameService {GetType().Name} Init, LifeSpan: {_lifeSpan}");
            return UniTask.CompletedTask;
        }

        public virtual void Update()
        {
        }

        public virtual void Release()
        {
            GameLog.Debug($"GameService {GetType().Name} Release, LifeSpan: {_lifeSpan}");
        }
    }
}
