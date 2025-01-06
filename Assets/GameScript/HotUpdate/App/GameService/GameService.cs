using Cysharp.Threading.Tasks;
using Game.Log;
using System;
using System.Reflection;
using UniFramework.Event;

namespace Game
{
    public class GameServiceAttribute : Attribute
    {
        public GameServiceDomain Domain { get; }

        public GameServiceAttribute(GameServiceDomain domain)
        {
            Domain = domain;
        }
    }

    public abstract class GameService
    {
        protected EventGroup _eventGroup = new EventGroup();

        public UniTask __Init()
        {
            GameLog.Debug($"{GetType().Name}:__Init");
            return Awake();
        }

        public UniTask __PostInit()
        {
            GameLog.Debug($"{GetType().Name}:__PostInit");
            return Start();
        }

        public void __Dispose()
        {
            GameLog.Debug($"{GetType().Name}:__Dispose");
            RemoveAllListener();
            OnDestroy();
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

        public virtual void LateUpdate()
        {
        }

        public virtual void FixedUpdate()
        {
        }

        protected virtual void OnDestroy()
        {
        }

        protected void AddEventListener<T>(Action<IEventMessage> action) where T : IEventMessage
        {
            if (_eventGroup == null)
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
