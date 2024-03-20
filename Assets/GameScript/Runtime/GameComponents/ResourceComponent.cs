using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UniFramework.Event;
using UnityEngine;
using YooAsset;

public class ResourceComponent : GameComponent
{
    /// <summary>
    /// 资源系统运行模式
    /// </summary>
    public EPlayMode PlayMode = EPlayMode.EditorSimulateMode;

    private const float UNLOAD_UNUSED_ASSETS_INTERVAL = 15f;
    private float _unloadUnusedAssetsCountdown = UNLOAD_UNUSED_ASSETS_INTERVAL;
    private ResourcePackage _gamePackage;

    #region 生命周期
    protected override async UniTask OnInitialize()
    {
        // 初始化事件系统
        UniEvent.Initalize();

        // 初始化资源系统
        YooAssets.Initialize();

        // 加载更新页面
        var go = Resources.Load<GameObject>("PatchWindow");
        GameObject.Instantiate(go);

        // 开始补丁更新流程
        PatchOperation operation = new PatchOperation("DefaultPackage", EDefaultBuildPipeline.BuiltinBuildPipeline.ToString(), PlayMode);
        YooAssets.StartOperation(operation);

        await operation.ToUniTask();

        // 设置默认的资源包
        _gamePackage = YooAssets.GetPackage("DefaultPackage");
        YooAssets.SetDefaultPackage(_gamePackage);

        // 切换到主页面场景
        SceneEventDefine.ChangeToHomeScene.SendEventMessage();
    }

    private void Update()
    {
        // 定时卸载未使用的资源
        if (_unloadUnusedAssetsCountdown > 0)
        {
            _unloadUnusedAssetsCountdown -= Time.deltaTime;
        }
        else
        {
            _gamePackage.UnloadUnusedAssets();
            _unloadUnusedAssetsCountdown += UNLOAD_UNUSED_ASSETS_INTERVAL;
        }
    }
    #endregion

    #region 加载资源
    public async UniTask<T> LoadAssetAsync<T>(string assetPath) where T : Object
    {
        var handle = YooAssets.LoadAssetAsync<T>(assetPath);
        await handle.ToUniTask();
        return handle.GetAssetObject<T>();
    }

    public T LoadAssetSync<T>(string assetPath) where T : Object
    {
        var handle = YooAssets.LoadAssetSync<T>(assetPath);
        return handle.GetAssetObject<T>();
    }
    #endregion
}
