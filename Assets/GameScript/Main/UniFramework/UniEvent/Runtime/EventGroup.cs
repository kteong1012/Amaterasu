using Cysharp.Threading.Tasks;
using Game.Log;
using System;
using System.Collections;
using System.Collections.Generic;

namespace UniFramework.Event
{
    public class EventGroup
    {
        private readonly Dictionary<System.Type, List<Action<IEventMessage>>> _cachedListener = new Dictionary<System.Type, List<Action<IEventMessage>>>();
        private readonly Dictionary<System.Type, List<Func<IAsyncEventMessage, UniTask>>> _cachedAsyncListener = new Dictionary<Type, List<Func<IAsyncEventMessage, UniTask>>>();

        /// <summary>
        /// 添加一个监听
        /// </summary>
        public void AddListener<TEvent>(System.Action<IEventMessage> listener) where TEvent : IEventMessage
        {
            System.Type eventType = typeof(TEvent);
            AddListener(eventType, listener);
        }
        /// <summary>
        /// 添加一个监听
        /// </summary>
        public void AddListener(Type eventType, System.Action<IEventMessage> listener)
        {
            if (_cachedListener.ContainsKey(eventType) == false)
                _cachedListener.Add(eventType, new List<Action<IEventMessage>>());

            if (_cachedListener[eventType].Contains(listener) == false)
            {
                _cachedListener[eventType].Add(listener);
                UniEvent.AddListener(eventType, listener);
            }
            else
            {
                GameLog.Warning($"Event listener is exist : {eventType}");
            }
        }

        /// <summary>
        /// 添加一个监听
        /// </summary>
        public void AddListener<TEvent>(System.Func<IAsyncEventMessage, UniTask> listener) where TEvent : IAsyncEventMessage
        {
            System.Type eventType = typeof(TEvent);
            if (_cachedAsyncListener.ContainsKey(eventType) == false)
                _cachedAsyncListener.Add(eventType, new List<Func<IAsyncEventMessage, UniTask>>());

            if (_cachedAsyncListener[eventType].Contains(listener) == false)
            {
                _cachedAsyncListener[eventType].Add(listener);
                UniEvent.AddAsyncListener(eventType, listener);
            }
            else
            {
                GameLog.Warning($"Event listener is exist : {eventType}");
            }
        }

        /// <summary>
        /// 移除所有缓存的监听
        /// </summary>
        public void RemoveAllListener()
        {
            foreach (var pair in _cachedListener)
            {
                System.Type eventType = pair.Key;
                for (int i = 0; i < pair.Value.Count; i++)
                {
                    UniEvent.RemoveListener(eventType, pair.Value[i]);
                }
                pair.Value.Clear();
            }
            _cachedListener.Clear();

            foreach (var pair in _cachedAsyncListener)
            {
                System.Type eventType = pair.Key;
                for (int i = 0; i < pair.Value.Count; i++)
                {
                    UniEvent.RemoveAsyncListener(eventType, pair.Value[i]);
                }
                pair.Value.Clear();
            }
            _cachedAsyncListener.Clear();
        }
    }
}