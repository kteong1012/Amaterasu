using cfg;
using Cysharp.Threading.Tasks;
using Game.Log;
using Luban;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game;

public class ConfigComponent : GameComponent
{
    public Tables Tables { get; private set; }
    protected override async UniTask OnInitialize()
    {
        Game.Log.GameLog.Info("====初始化配置====");
        Tables = new Tables();
        await Tables.LoadAll(Loader);
        Game.Log.GameLog.Info("====初始化配置完成====");
    }

    private async UniTask<ByteBuf> Loader(string tableName)
    {
        var resourceComponent = G.Ins.GetGameComponent<ResourceComponent>();
        var asset = await resourceComponent.LoadAssetAsync<TextAsset>($"{tableName}");
        return new ByteBuf(asset.bytes);
    }
}
