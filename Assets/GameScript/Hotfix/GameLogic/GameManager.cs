using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniFramework.Event;
using YooAsset;
using Cysharp.Threading.Tasks;
using Game.Log;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private readonly EventGroup _eventGroup = new EventGroup();

    public ResourceComponent Resource { get; private set; }
    public ConfigComponent Config { get; private set; }

    private async void Awake()
    {
        Instance = this;
        Application.targetFrameRate = 60;
        Application.runInBackground = true;
        DontDestroyOnLoad(this.gameObject);

        // 初始化Log
        Log.RegisterLogger(UnityConsoleLog.Instance);

        // 注册监听事件
        _eventGroup.AddListener<SceneEventDefine.ChangeToHomeScene>(OnHandleEventMessage);
        _eventGroup.AddListener<SceneEventDefine.ChangeToBattleScene>(OnHandleEventMessage);

        // 初始化游戏组件
        await InitGameComponents();
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

    private async UniTask InitGameComponents()
    {
        async UniTask<T> GetGameComponent<T>() where T : GameComponent
        {
            var component = transform.GetComponentInChildren<T>();
            if (component == null)
            {
                throw new System.Exception($"GameComponent {typeof(T).Name} not found");
            }
            await component.Initialize();
            return component;
        }

        Resource = await GetGameComponent<ResourceComponent>();
        Config = await GetGameComponent<ConfigComponent>();
    }
}