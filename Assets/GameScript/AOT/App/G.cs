using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniFramework.Event;
using YooAsset;
using Cysharp.Threading.Tasks;
using Game.Log;
using UniFramework.Machine;

namespace Game
{
    public class G : MonoBehaviour
    {
        public static G Ins { get; private set; }

        private Dictionary<System.Type, GameModule> _gameModules = new Dictionary<System.Type, GameModule>();

        private readonly EventGroup _eventGroup = new EventGroup();

        private StateMachine _machine;

        private void Awake()
        {
            Ins = this;
            Application.targetFrameRate = 60;
            Application.runInBackground = true;
            DontDestroyOnLoad(this.gameObject);

            // 初始化Log
            GameLog.RegisterLogger(UnityConsoleLog.Instance);

            // 创建状态机
            _machine = new StateMachine(this);
            _machine.AddNode<FsmInitializeAppConst>();
            _machine.AddNode<FsmInitializeResourceModule>();
            _machine.AddNode<FsmGameLoop>();
            _machine.AddNode<FsmFinishApp>();

            _machine.Run<FsmInitializeAppConst>();
        }

        private void Update()
        {
            _machine.Update();
        }

        private void OnDestroy()
        {
            _machine.ChangeState<FsmFinishApp>();

            _eventGroup.RemoveAllListener();

            foreach (var (_, comp) in _gameModules)
            {
                comp.Release();
            }
            _gameModules.Clear();

            GameLog.ClearLogger();
        }
        public async UniTask<T> RegisterGameModule<T>() where T : GameModule
        {
            var module = Get<T>();
            if (module != null)
            {
                GameLog.Warning($"GameModule {typeof(T).Name} already exists");
                return module;
            }
            module = CreateGameModule<T>();
            await module.Initialize();

            _gameModules.Add(typeof(T), module);
            return module;
        }

        private T CreateGameModule<T>() where T : GameModule
        {
            var go = new GameObject(typeof(T).Name);
            go.transform.SetParent(transform);
            return go.AddComponent<T>();
        }

        public T Get<T>() where T : GameModule
        {
            if (_gameModules.TryGetValue(typeof(T), out var module))
            {
                return module as T;
            }
            return null;
        }
    }
}