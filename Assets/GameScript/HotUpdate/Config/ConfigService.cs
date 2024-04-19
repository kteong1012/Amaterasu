using Cysharp.Threading.Tasks;
using Game.Log;
using Luban;
using System.Collections.Generic;
using UnityEngine;
using YooAsset;

namespace Game.Cfg
{

    [GameService(GameServiceLifeSpan.Game)]
    public partial class ConfigService : GameService
    {
        private List<AssetHandle> _assetHandles = new List<AssetHandle>();
        public override async UniTask Init()
        {
            GameLog.Info("====初始化配置====");
            await LoadAll(Loader);
            _assetHandles.ForEach(handle => handle.Release());
            GameLog.Info("====初始化配置完成====");
        }

        private async UniTask<ByteBuf> Loader(string tableName)
        {
            GameLog.Debug($"加载配置表 {tableName}");
            var handle = YooAssets.LoadAssetAsync($"{tableName}");
            await handle.ToUniTask();
            var textAsset = handle.GetAssetObject<TextAsset>();
            var bytes = textAsset.bytes;
            _assetHandles.Add(handle);
            return new ByteBuf(bytes);
        }
    }
}
