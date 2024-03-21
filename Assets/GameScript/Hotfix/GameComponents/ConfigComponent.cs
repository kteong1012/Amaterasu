using cfg;
using Cysharp.Threading.Tasks;
using Game.Log;
using Luban;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game;
using YooAsset;

public class ConfigComponent : GameComponent
{
    public Tables Tables { get; private set; }
    private List<AssetHandle> _assetHandles = new List<AssetHandle>();
    protected override async UniTask OnInitialize()
    {
        Game.Log.GameLog.Info("====初始化配置====");
        Tables = new Tables();
        await Tables.LoadAll(Loader);
        _assetHandles.ForEach(handle => handle.Release());
        Game.Log.GameLog.Info("====初始化配置完成====");
    }

    private async UniTask<ByteBuf> Loader(string tableName)
    {
        var handle = YooAssets.LoadAssetAsync($"{tableName}");
        await handle.ToUniTask();
        var textAsset = handle.GetAssetObject<TextAsset>();
        var bytes = textAsset.bytes;
        _assetHandles.Add(handle);
        return new ByteBuf(bytes);
    }
}
