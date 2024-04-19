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
        public GameServiceLifeSpan LifeSpan => GetType().GetCustomAttribute<GameServiceAttribute>().LifeSpan;
        public UniTask Init()
        {
            GameLog.Debug($"GameService {GetType().Name} Init, LifeSpan: {LifeSpan}");
            return OnInit();
        }
        protected virtual UniTask OnInit()
        {
            GameLog.Debug($"GameService {GetType().Name} Init, LifeSpan: {LifeSpan}");
            return UniTask.CompletedTask;
        }

        public virtual void Update()
        {
        }

        public void Release()
        {
            GameLog.Debug($"GameService {GetType().Name} Release, LifeSpan: {LifeSpan}");
            OnRelease();
        }
        protected virtual void OnRelease()
        {
        }
    }
}
