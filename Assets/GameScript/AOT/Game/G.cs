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

        private Dictionary<System.Type, GameComponent> _gameComponents = new Dictionary<System.Type, GameComponent>();

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

            // 注册监听事件
            _eventGroup.AddListener<SceneEventDefine.ChangeToHomeScene>(OnHandleEventMessage);
            _eventGroup.AddListener<SceneEventDefine.ChangeToBattleScene>(OnHandleEventMessage);

            // 创建状态机
            _machine = new StateMachine(this);
            _machine.AddNode<FsmInitializeAppConst>();
            _machine.AddNode<FsmInitializeResourceComponent>();
            _machine.AddNode<FsmLoadDll>();
            _machine.Run<FsmInitializeAppConst>();
        }

        private void Update()
        {
            _machine.Update();
        }

        private void OnDestroy()
        {
            GameLog.ClearLogger();

            _eventGroup.RemoveAllListener();

            foreach (var (_, comp) in _gameComponents)
            {
                comp.Release();
            }
            _gameComponents.Clear();
        }

        /// <summary>
        /// 接收事件
        /// </summary>
        private void OnHandleEventMessage(IEventMessage message)
        {
            if (message is SceneEventDefine.ChangeToHomeScene)
            {
                YooAssets.LoadSceneAsync("scene_home");
            }
            else if (message is SceneEventDefine.ChangeToBattleScene)
            {
                YooAssets.LoadSceneAsync("scene_battle");
            }
        }
        public async UniTask<T> RegisterGameComponent<T>() where T : GameComponent
        {
            var component = Get<T>();
            if (component != null)
            {
                GameLog.Warning($"GameComponent {typeof(T).Name} already exists");
                return component;
            }
            component = CreateGameComponent<T>();
            await component.Initialize();

            _gameComponents.Add(typeof(T), component);
            return component;
        }

        private T CreateGameComponent<T>() where T : GameComponent
        {
            var go = new GameObject(typeof(T).Name);
            go.transform.SetParent(transform);
            return go.AddComponent<T>();
        }

        public T Get<T>() where T : GameComponent
        {
            if (_gameComponents.TryGetValue(typeof(T), out var component))
            {
                return component as T;
            }
            return null;
        }
    }
}