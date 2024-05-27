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
        public GameServiceDomain Domain => GetType().GetCustomAttribute<GameServiceAttribute>().Domain;
        protected EventGroup _eventGroup = new EventGroup();

        public UniTask Init()
        {
            GameLog.Debug($"GameService {GetType().Name} Init, Domain: {Domain}");
            return Awake();
        }
        public UniTask PostInit()
        {
            GameLog.Debug($"GameService {GetType().Name} PostInit, Domain: {Domain}");
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
            GameLog.Debug($"GameService {GetType().Name} Destroy, Domain: {Domain}");
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
