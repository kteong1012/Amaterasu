using Cysharp.Threading.Tasks;
using Game.Log;
using System;
using System.Reflection;
using UniFramework.Event;

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
        protected EventGroup _eventGroup;

        public UniTask Init()
        {
            GameLog.Debug($"GameService {GetType().Name} Init, LifeSpan: {LifeSpan}");
            return Awake();
        }
        public UniTask PostInit()
        {
            GameLog.Debug($"GameService {GetType().Name} PostInit, LifeSpan: {LifeSpan}");
            return Start();
        }
        protected virtual UniTask Awake()
        {
            return UniTask.CompletedTask;
        }
        protected virtual UniTask Start()
        {
            return UniTask.CompletedTask;
        }

        public virtual void Update()
        {
        }

        public void Destroy()
        {
            GameLog.Debug($"GameService {GetType().Name} Destroy, LifeSpan: {LifeSpan}");
            OnDestroy();
        }
        protected virtual void OnDestroy()
        {
        }

        protected void AddEventListener<T>(Action<IEventMessage> action) where T : IEventMessage
        {
            if(_eventGroup == null)
            {
                _eventGroup = new EventGroup();
            }
            _eventGroup.AddListener<T>(action);
        }

        protected void RemoveAllListener()
        {
            _eventGroup?.RemoveAllListener();
        }
    }
}
